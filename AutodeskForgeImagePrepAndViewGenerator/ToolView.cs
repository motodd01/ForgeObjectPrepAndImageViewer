using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace AutodeskForgeObjectPrepAndViewGenerator
{
    // Based off of example here: https://github.com/Autodesk-Forge/bucket.manager-csharp-sample.tool
    public interface IToolView
    {
        string Text { get; set; }
        event EventHandler GetBucketsPressed;
        event EventHandler<string> CreateBucketPressed;
        event EventHandler<string> UploadFilePressed;
        event EventHandler<string> TranslatePressed;
        event EventHandler<string> ShowInBrowserPressed;
        event EventHandler ObjectSelected;
        event EventHandler<string> BucketSelected;
        void SubscribeToEvents();
        string GetObjectFilePathFromUser();
        void ShowMessage(string msg);
        void ClearBuckets();
        void ShowBuckets(List<string> buckets);
        void ClearObjects();
        void ShowInBrowser(string urn, string accessToken);
        void ShowObjectOperationsEnabled(bool enable);
        void ShowBucketOperationsEnabled();
        void ShowObjects(List<string> objects);
        void ShowObjectUploadPath(string filePath);
        void OnUploadFilePressed(object sender, EventArgs e);
        void OnTranslatePressed(object sender, EventArgs e);
        void OnShowInBrowserPressed(object sender, EventArgs e);
        void OnBucketSelected(object sender, EventArgs e);
        void OnCreateBucketPressed(object sender, EventArgs e);

        void ShowProcessing();

        void ShowProcessingComplete();
    }

    public partial class ToolView : Form, IToolView
    {
        public ToolView()
        {
            InitializeComponent();
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public event EventHandler GetBucketsPressed;
        public event EventHandler<string> CreateBucketPressed;
        public event EventHandler<string> UploadFilePressed;
        public event EventHandler<string> TranslatePressed;
        public event EventHandler<string> ShowInBrowserPressed;
        public event EventHandler ObjectSelected;
        public event EventHandler<string> BucketSelected;

        public void SubscribeToEvents()
        {
            btnGetBuckets.Click += GetBucketsPressed;
            btnCreateBucket.Click += OnCreateBucketPressed;
            btnUploadFile.Click += OnUploadFilePressed;
            btnTranslate.Click += OnTranslatePressed;
            btnShowInBrowser.Click += OnShowInBrowserPressed;
            comboBoxObjects.SelectedIndexChanged += ObjectSelected;
            comboBoxBuckets.SelectedIndexChanged += OnBucketSelected;
        }

        public string GetObjectFilePathFromUser()
        {
            var openFileDialog = new OpenFileDialog {Multiselect = false};
            return openFileDialog.ShowDialog() != DialogResult.OK ? null : openFileDialog.FileName;
        }

        public void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        public void ClearBuckets()
        {
            this.InvokeIfRequired(() => { comboBoxBuckets.Items.Clear(); });
        }

        public void ShowBuckets(List<string> buckets)
        {
            this.InvokeIfRequired(() =>
            {
                foreach (var bucketName in buckets)
                    comboBoxBuckets.Items.Add(bucketName);

                btnTranslate.Enabled = false;
                btnShowInBrowser.Enabled = false;
                comboBoxBuckets.Enabled = true;
                comboBoxObjects.Enabled = false;
                btnUploadFile.Enabled = false;
            });
        }

        public void ShowObjects(List<string> objects)
        {
            this.InvokeIfRequired(() =>
            {
                comboBoxObjects.Items.Clear();
                foreach (var objectName in objects)
                    comboBoxObjects.Items.Add(objectName);
            });
        }

        public void ClearObjects()
        {
            this.InvokeIfRequired(() => { comboBoxObjects.Items.Clear(); });
        }

        public void ShowInBrowser(string urn, string accessToken)
        {
            Process.Start("Chrome", string.Format("file:///{0}/ForgeViewer.html?URN={1}&Token={2}",
                Application.StartupPath, urn, accessToken));
        }

        public void ShowObjectOperationsEnabled(bool enable)
        {
            this.InvokeIfRequired(() =>
            {
                comboBoxObjects.Enabled = enable;
                btnTranslate.Enabled = enable;
                btnShowInBrowser.Enabled = enable;
            });
        }

        public void ShowBucketOperationsEnabled()
        {
            this.InvokeIfRequired(() =>
            {
                comboBoxObjects.Enabled = true;
                btnUploadFile.Enabled = true;
            });
        }

        public void ShowObjectUploadPath(string filePath)
        {
            this.InvokeIfRequired(() => { textBoxSelectedPath.Text = filePath; });
        }

        public void OnUploadFilePressed(object sender, EventArgs e)
        {
            var uploadPath = comboBoxBuckets.SelectedItem.ToString();
            if (UploadFilePressed != null)
                UploadFilePressed.Invoke(this, uploadPath);
        }

        public void OnTranslatePressed(object sender, EventArgs e)
        {
            var objectName = comboBoxObjects.SelectedItem.ToString();
            if (TranslatePressed != null)
                TranslatePressed.Invoke(this, objectName);
        }

        public void OnShowInBrowserPressed(object sender, EventArgs e)
        {
            var objectName = comboBoxObjects.SelectedItem.ToString();
            if (ShowInBrowserPressed != null)
                ShowInBrowserPressed.Invoke(this, objectName);
        }

        public void OnBucketSelected(object sender, EventArgs e)
        {
            var bucketName = comboBoxBuckets.SelectedItem.ToString();
            if (BucketSelected != null)
                BucketSelected.Invoke(this, bucketName);
        }

        public void OnCreateBucketPressed(object sender, EventArgs e)
        {
            if (CreateBucketPressed != null)
                CreateBucketPressed.Invoke(this, textBoxNewBucketName.Text);
        }

        public void ShowProcessing()
        {
            this.InvokeIfRequired(() =>
            {
                progressBar.Visible = true;
                labelStatus.Text = @"Processing...";
            });
        }

        public void ShowProcessingComplete()
        {
            this.InvokeIfRequired(() =>
            {
                progressBar.Visible = false;
                labelStatus.Text = @"Idle";
            });
        }
    }
}