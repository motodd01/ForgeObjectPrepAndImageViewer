using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autodesk.Forge;
using Autodesk.Forge.Client;
using Autodesk.Forge.Model;

namespace AutodeskForgeObjectPrepAndViewGenerator
{
    public interface IAuthenticationToken
    {
        string AccessToken { get; }
        bool IsExpired { get; }
    }

    public class AuthenticationToken : IAuthenticationToken
    {
        DateTime _expirationDateTime;

        public AuthenticationToken(string accessToken, long expirationTimeInSec)
        {
            _expirationDateTime = DateTime.Now.AddSeconds(expirationTimeInSec);
            AccessToken = accessToken;
        }

        public string AccessToken { get; private set; }

        public bool IsExpired
        {
            get { return _expirationDateTime.CompareTo(DateTime.Now) > 0; }
        }
    }

    public class ForgeObject
    {
        public ForgeObject(string objectKey, string objectId)
        {
            ObjectKey = objectKey;
            ObjectId = objectId;
        }

        public string ObjectKey { get; private set; }
        public string ObjectId { get; private set; }
    }

    public interface IForgeClient
    {
        IAuthenticationToken AuthenticationToken { get; }
        Task<List<string>> GetBuckets();
        Task<List<ForgeObject>> GetObjectsForBucket(string bucketKey);
        Task<bool> CreateBucket(string bucketKey);
        Task<bool> UploadFile(string filePath, string bucketKey);
        Task<bool> TranslateFile(string urn);
        string GetUrnFromSelectedObject(string objectName);
        Task<bool> WaitForTranslationComplete(string urn, int timeoutSec, int delayBetweenCheckMs);

    }

    public class ForgeClient : IForgeClient
    {
        readonly string _clientId;
        readonly string _clientSecret;
        readonly ILogger _logger;
        List<ForgeObject> _objectList;

        public ForgeClient(string clientId, string clientSecret)
        {
            if (clientId == null) throw new ArgumentNullException("clientId");
            if (clientSecret == null) throw new ArgumentNullException("clientSecret");

            _clientId = clientId;
            _clientSecret = clientSecret;

            _logger = new Logger(GetType().Name);
        }

        public string GetUrnFromSelectedObject(string objectName)
        {
            var forgeObject = GetObjectByKey(objectName); //comboBoxObjects.SelectedItem.ToString()
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(forgeObject.ObjectId));
        }

        public async Task<List<string>> GetBuckets()
        {
            var isAuthorized = await IsAuthorized();
            if (!isAuthorized)
            {
                _logger.Log(LogType.Error, "Cannot be authorized for getting buckets");
                return null;
            }

            var bucketList = new List<string>();
            var bucketApi = new BucketsApi {Configuration = {AccessToken = AuthenticationToken.AccessToken}};
            dynamic buckets;
            try
            {
                buckets = await bucketApi.GetBucketsAsync();
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, "Exception when getting Buckets: " + e);
                return null;
            }

            foreach (KeyValuePair<string, dynamic> bucket in new DynamicDictionaryItems(buckets.items))
                bucketList.Add(bucket.Value.bucketKey);

            return bucketList;
        }

        public async Task<List<ForgeObject>> GetObjectsForBucket(string bucketKey)
        {
            var isAuthorized = await IsAuthorized();
            if (!isAuthorized)
            {
                _logger.Log(LogType.Error, "Cannot be authorized for getting objects");
                return null;
            }

            _objectList = new List<ForgeObject>();
            var objectsApi = new ObjectsApi {Configuration = {AccessToken = AuthenticationToken.AccessToken}};
            dynamic objects;

            try
            {
                objects = await objectsApi.GetObjectsAsync(bucketKey);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, "Exception when getting Objects: " + e);
                return null;
            }

            foreach (KeyValuePair<string, dynamic> bucket in new DynamicDictionaryItems(objects.items))
                _objectList.Add(new ForgeObject(bucket.Value.objectKey, bucket.Value.objectId));

            return _objectList;
        }

        public async Task<bool> CreateBucket(string bucketKey)
        {
            if (bucketKey == null) throw new ArgumentNullException("bucketKey");

            var isAuthorized = await IsAuthorized();
            if (!isAuthorized)
            {
                _logger.Log(LogType.Error, "Cannot be authorized for creating bucket");
                return false;
            }

            var bucketsApi = new BucketsApi {Configuration = {AccessToken = AuthenticationToken.AccessToken}};
            var postBuckets = new PostBucketsPayload(bucketKey.ToLower(), null,
                PostBucketsPayload.PolicyKeyEnum.Transient);

            try
            {
                await bucketsApi.CreateBucketAsync(postBuckets);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, "Exception when creating the Bucket: " + e);
                return false;
            }

            return true;
        }

        public async Task<bool> UploadFile(string filePath, string bucketKey)
        {
            if (filePath == null) throw new ArgumentNullException("filePath");
            if (bucketKey == null) throw new ArgumentNullException("bucketKey");

            if (!File.Exists(filePath))
            {
                _logger.Log(LogType.Error, "Unable to find file: " + filePath);
                return false;
            }

            var isAuthorized = await IsAuthorized();
            if (!isAuthorized)
            {
                _logger.Log(LogType.Error, "Cannot be authorized for uploading file");
                return false;
            }

            var fileName = Path.GetFileName(filePath);
            var objectsApi = new ObjectsApi {Configuration = {AccessToken = AuthenticationToken.AccessToken}};

            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    dynamic uploadedObj = await objectsApi.UploadObjectAsync(bucketKey,
                        fileName, (int) streamReader.BaseStream.Length, streamReader.BaseStream,
                        "application/octet-stream");
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogType.Error, "Exception when uploading the file: " + ex);
                return false;
            }

            return true;
        }

        public async Task<bool> TranslateFile(string urn)
        {
            if (urn == null) throw new ArgumentNullException("urn");

            var isAuthorized = await IsAuthorized();
            if (!isAuthorized)
            {
                _logger.Log(LogType.Error, "Cannot be authorized for translating file");
                return false;
            }

            var jobPayloadItems = new List<JobPayloadItem>
            {
                new JobPayloadItem(
                    JobPayloadItem.TypeEnum.Svf,
                    new List<JobPayloadItem.ViewsEnum>
                    {
                        JobPayloadItem.ViewsEnum._2d,
                        JobPayloadItem.ViewsEnum._3d
                    })
            };

            var job = new JobPayload(new JobPayloadInput(urn), new JobPayloadOutput(jobPayloadItems));

            var derivative = new DerivativesApi
            {
                Configuration = {AccessToken = AuthenticationToken.AccessToken}
            };

            try
            {
                dynamic jobPosted = await derivative.TranslateAsync(job);
            }
            catch (Exception ex)
            {
                _logger.Log(LogType.Error, "Exception while translating file: " + ex);
                return false;
            }

            return true;
        }

        public IAuthenticationToken AuthenticationToken { get; private set; }

        public async Task<bool> WaitForTranslationComplete(string urn, int timeoutSec, int delayBetweenCheckMs)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.Elapsed.TotalSeconds < timeoutSec)
            {
                var translationComplete = await IsTranslationComplete(urn);
                if (translationComplete) return true;
                Thread.Sleep(delayBetweenCheckMs);
            }
            stopwatch.Stop();

            return false;
        }

        async Task<bool> IsTranslationComplete(string urn)
        {
            if (urn == null) throw new ArgumentNullException("urn");

            var isAuthorized = await IsAuthorized();
            if (!isAuthorized)
            {
                _logger.Log(LogType.Error, "Cannot be authorized for checking translation complete");
                return false;
            }

            var derivative = new DerivativesApi
            {
                Configuration = {AccessToken = AuthenticationToken.AccessToken}
            };

            // get the translation manifest
            dynamic manifest;

            try
            {
                manifest = await derivative.GetManifestAsync(urn);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, "Exception while for checking translation complete: " + e);
                return false;
            }
            
            return manifest.status == "success";
        }

        ForgeObject GetObjectByKey(string key)
        {
            return _objectList.FirstOrDefault(obj => obj.ObjectKey == key);
        }

        async Task<bool> IsAuthorized()
        {
            if ((AuthenticationToken == null) || AuthenticationToken.IsExpired)
                AuthenticationToken = await GetAuthToken();

            return AuthenticationToken != null;
        }

        async Task<IAuthenticationToken> GetAuthToken()
        {
            var twoLeggedApi = new TwoLeggedApi();
            ApiResponse<dynamic> apiResponse;
            try
            {
                apiResponse =
                    await twoLeggedApi.AuthenticateAsyncWithHttpInfo(_clientId, _clientSecret,
                        oAuthConstants.CLIENT_CREDENTIALS,
                        new[] {Scope.BucketRead, Scope.BucketCreate, Scope.DataRead, Scope.DataWrite});

                if (apiResponse == null)
                {
                    _logger.Log(LogType.Error, "NULL API response during authorization");
                    return null;
                }

                if (apiResponse.StatusCode != 200)
                {
                    _logger.Log(LogType.Error,
                        "API response not expected during authorization: " + apiResponse.StatusCode);
                    return null;
                }

                Configuration.Default.AccessToken = apiResponse.Data.access_token;
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, "Exception while attempting to get access token: " + e);
                return null;
            }

            return new AuthenticationToken(apiResponse.Data.access_token, apiResponse.Data.expires_in);
        }
    }
}