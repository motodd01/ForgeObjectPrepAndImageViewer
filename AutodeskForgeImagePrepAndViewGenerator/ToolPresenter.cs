using System;
using System.Linq;

namespace AutodeskForgeObjectPrepAndViewGenerator
{
    public interface IToolPresenter
    {
        void Run();
    }

    public class ToolPresenter : IToolPresenter
    {
        readonly IForgeClient _model;
        readonly IToolView _view;

        public ToolPresenter(IToolView view, IForgeClient model)
        {
            _view = view;
            _model = model;
        }

        public void Run()
        {
            Initialize();
            _view.SubscribeToEvents();
        }

        void Initialize()
        {
            _view.GetBucketsPressed += OnGetBucketsPressed;
            _view.CreateBucketPressed += OnCreateBucketsPressed;
            _view.UploadFilePressed += OnUploadFilePressed;
            _view.TranslatePressed += OnTranslatePressed;
            _view.ShowInBrowserPressed += OnShowInBrowserPressed;
            _view.ObjectSelected += OnObjectSelected;
            _view.BucketSelected += OnBucketSelected;
        }

        void OnObjectSelected(object sender, EventArgs e)
        {
            _view.ShowObjectOperationsEnabled(true);
        }

        void OnShowInBrowserPressed(object sender, string objectName)
        {
            var urn = _model.GetUrnFromSelectedObject(objectName);
            var accessToken = _model.AuthenticationToken.AccessToken;
            _view.ShowInBrowser(urn, accessToken);
        }

        async void OnBucketSelected(object sender, string bucketName)
        {
            var objects = await _model.GetObjectsForBucket(bucketName);
            if (objects == null)
            {
                _view.ShowMessage(@"No Objects for Bucket: " + bucketName);
                return;
            }

            var objectNames = objects.Select(forgeObject => forgeObject.ObjectKey).ToList();
            _view.ShowObjects(objectNames);

            _view.ShowObjectOperationsEnabled(false);
            _view.ShowBucketOperationsEnabled();
        }

        async void OnTranslatePressed(object sender, string objectName)
        {
            _view.ShowProcessing();
            var urn = _model.GetUrnFromSelectedObject(objectName);
            var translateOk = await _model.TranslateFile(urn);

            var translationComplete = false;
            if (translateOk)
                translationComplete = await _model.WaitForTranslationComplete(urn, 120, 500);

            _view.ShowProcessingComplete();

            _view.ShowMessage(translateOk && translationComplete ? @"File Translated" : @"File Translation Failed!");
        }

        async void OnUploadFilePressed(object sender, string bucketKey)
        {
            var path = _view.GetObjectFilePathFromUser();
            if (path == null) return;

            _view.ShowProcessing();

            _view.ShowObjectUploadPath(path);

            var uploadOk = await _model.UploadFile(path, bucketKey);
            if (uploadOk) OnBucketSelected(this, bucketKey);
            _view.ShowProcessingComplete();

            _view.ShowMessage(uploadOk ? @"File Uploaded" : @"File Upload Failed!");
        }

        async void OnCreateBucketsPressed(object sender, string bucketName)
        {
            _view.ShowProcessing();
            var bucketCreationResult = await _model.CreateBucket(bucketName);
            _view.ShowProcessingComplete();

            _view.ShowMessage(bucketCreationResult ? @"Bucket Created" : @"Bucket Creation Failed!");
        }

        async void OnGetBucketsPressed(object sender, EventArgs e)
        {
            _view.ShowProcessing();
            var bucketNames = await _model.GetBuckets();
            _view.ShowProcessingComplete();
            if (bucketNames == null)
            {
                _view.ShowMessage(@"No Buckets returned");
                return;
            }

            _view.ClearObjects();
            _view.ClearBuckets();

            _view.ShowBuckets(bucketNames);

            _view.ShowMessage(bucketNames.Count + @" Buckets returned");
        }
    }
}