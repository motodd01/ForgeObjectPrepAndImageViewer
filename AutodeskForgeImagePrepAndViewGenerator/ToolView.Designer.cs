namespace AutodeskForgeObjectPrepAndViewGenerator
{
    partial class ToolView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreateBucket = new System.Windows.Forms.Button();
            this.textBoxNewBucketName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxBucketAndObjectCreation = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxBuckets = new System.Windows.Forms.ComboBox();
            this.btnGetBuckets = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSelectedPath = new System.Windows.Forms.TextBox();
            this.btnUploadFile = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxObjects = new System.Windows.Forms.ComboBox();
            this.btnShowInBrowser = new System.Windows.Forms.Button();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelStatus = new System.Windows.Forms.Label();
            this.groupBoxBucketAndObjectCreation.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateBucket
            // 
            this.btnCreateBucket.Location = new System.Drawing.Point(252, 28);
            this.btnCreateBucket.Name = "btnCreateBucket";
            this.btnCreateBucket.Size = new System.Drawing.Size(112, 32);
            this.btnCreateBucket.TabIndex = 1;
            this.btnCreateBucket.Text = "Create Bucket";
            this.btnCreateBucket.UseVisualStyleBackColor = true;
            // 
            // textBoxNewBucketName
            // 
            this.textBoxNewBucketName.Location = new System.Drawing.Point(6, 35);
            this.textBoxNewBucketName.Name = "textBoxNewBucketName";
            this.textBoxNewBucketName.Size = new System.Drawing.Size(239, 20);
            this.textBoxNewBucketName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "New Bucket Name";
            // 
            // groupBoxBucketAndObjectCreation
            // 
            this.groupBoxBucketAndObjectCreation.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBoxBucketAndObjectCreation.Controls.Add(this.label3);
            this.groupBoxBucketAndObjectCreation.Controls.Add(this.textBoxNewBucketName);
            this.groupBoxBucketAndObjectCreation.Controls.Add(this.comboBoxBuckets);
            this.groupBoxBucketAndObjectCreation.Controls.Add(this.btnCreateBucket);
            this.groupBoxBucketAndObjectCreation.Controls.Add(this.label1);
            this.groupBoxBucketAndObjectCreation.Controls.Add(this.btnGetBuckets);
            this.groupBoxBucketAndObjectCreation.Location = new System.Drawing.Point(12, 5);
            this.groupBoxBucketAndObjectCreation.Name = "groupBoxBucketAndObjectCreation";
            this.groupBoxBucketAndObjectCreation.Size = new System.Drawing.Size(364, 259);
            this.groupBoxBucketAndObjectCreation.TabIndex = 14;
            this.groupBoxBucketAndObjectCreation.TabStop = false;
            this.groupBoxBucketAndObjectCreation.Text = "Bucket Operations";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Select A Bucket";
            // 
            // comboBoxBuckets
            // 
            this.comboBoxBuckets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBuckets.Enabled = false;
            this.comboBoxBuckets.FormattingEnabled = true;
            this.comboBoxBuckets.Location = new System.Drawing.Point(6, 189);
            this.comboBoxBuckets.Name = "comboBoxBuckets";
            this.comboBoxBuckets.Size = new System.Drawing.Size(352, 21);
            this.comboBoxBuckets.TabIndex = 17;
            // 
            // btnGetBuckets
            // 
            this.btnGetBuckets.Location = new System.Drawing.Point(6, 94);
            this.btnGetBuckets.Name = "btnGetBuckets";
            this.btnGetBuckets.Size = new System.Drawing.Size(352, 32);
            this.btnGetBuckets.TabIndex = 16;
            this.btnGetBuckets.Text = "Get Buckets";
            this.btnGetBuckets.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Local Selected Object Path";
            // 
            // textBoxSelectedPath
            // 
            this.textBoxSelectedPath.Location = new System.Drawing.Point(6, 36);
            this.textBoxSelectedPath.Name = "textBoxSelectedPath";
            this.textBoxSelectedPath.Size = new System.Drawing.Size(239, 20);
            this.textBoxSelectedPath.TabIndex = 16;
            // 
            // btnUploadFile
            // 
            this.btnUploadFile.Enabled = false;
            this.btnUploadFile.Location = new System.Drawing.Point(251, 29);
            this.btnUploadFile.Name = "btnUploadFile";
            this.btnUploadFile.Size = new System.Drawing.Size(113, 32);
            this.btnUploadFile.TabIndex = 15;
            this.btnUploadFile.Text = "Upload File...";
            this.btnUploadFile.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(88, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Select An Object From Selected Bucket";
            // 
            // comboBoxObjects
            // 
            this.comboBoxObjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxObjects.Enabled = false;
            this.comboBoxObjects.FormattingEnabled = true;
            this.comboBoxObjects.Location = new System.Drawing.Point(6, 90);
            this.comboBoxObjects.Name = "comboBoxObjects";
            this.comboBoxObjects.Size = new System.Drawing.Size(358, 21);
            this.comboBoxObjects.TabIndex = 18;
            // 
            // btnShowInBrowser
            // 
            this.btnShowInBrowser.Enabled = false;
            this.btnShowInBrowser.Location = new System.Drawing.Point(6, 189);
            this.btnShowInBrowser.Name = "btnShowInBrowser";
            this.btnShowInBrowser.Size = new System.Drawing.Size(358, 32);
            this.btnShowInBrowser.TabIndex = 15;
            this.btnShowInBrowser.Text = "View Selected Object In Chrome";
            this.btnShowInBrowser.UseVisualStyleBackColor = true;
            // 
            // btnTranslate
            // 
            this.btnTranslate.Enabled = false;
            this.btnTranslate.Location = new System.Drawing.Point(6, 136);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(358, 32);
            this.btnTranslate.TabIndex = 14;
            this.btnTranslate.Text = "Translate Selected Object";
            this.btnTranslate.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxObjects);
            this.groupBox1.Controls.Add(this.btnTranslate);
            this.groupBox1.Controls.Add(this.btnShowInBrowser);
            this.groupBox1.Controls.Add(this.textBoxSelectedPath);
            this.groupBox1.Controls.Add(this.btnUploadFile);
            this.groupBox1.Location = new System.Drawing.Point(382, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 259);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Object Operations";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 288);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(740, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 19;
            this.progressBar.Visible = false;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(365, 314);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(35, 20);
            this.labelStatus.TabIndex = 20;
            this.labelStatus.Text = "Idle";
            // 
            // ToolView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 355);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxBucketAndObjectCreation);
            this.Name = "ToolView";
            this.ShowIcon = false;
            this.Text = "Forge Translate And View";
            this.groupBoxBucketAndObjectCreation.ResumeLayout(false);
            this.groupBoxBucketAndObjectCreation.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateBucket;
        private System.Windows.Forms.TextBox textBoxNewBucketName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxBucketAndObjectCreation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSelectedPath;
        private System.Windows.Forms.Button btnUploadFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxObjects;
        private System.Windows.Forms.ComboBox comboBoxBuckets;
        private System.Windows.Forms.Button btnGetBuckets;
        private System.Windows.Forms.Button btnShowInBrowser;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelStatus;
    }
}

