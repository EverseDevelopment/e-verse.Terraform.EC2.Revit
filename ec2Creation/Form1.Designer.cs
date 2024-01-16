namespace TerraformRunner
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnRunTerraform = new Button();
            txtOutput = new TextBox();
            btnDirectorySelector = new Button();
            project_fbg = new FolderBrowserDialog();
            lblSelectedPath = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            progressBar1 = new ProgressBar();
            backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            lblTerraformVersion = new Label();
            SuspendLayout();
            // 
            // btnRunTerraform
            // 
            btnRunTerraform.Location = new Point(210, 26);
            btnRunTerraform.Name = "btnRunTerraform";
            btnRunTerraform.Size = new Size(168, 23);
            btnRunTerraform.TabIndex = 0;
            btnRunTerraform.Text = "Terrform Run";
            btnRunTerraform.UseVisualStyleBackColor = true;
            btnRunTerraform.Click += btnRunTerraform_Click;
            // 
            // txtOutput
            // 
            txtOutput.Location = new Point(26, 75);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.ScrollBars = ScrollBars.Vertical;
            txtOutput.Size = new Size(588, 295);
            txtOutput.TabIndex = 1;
            // 
            // btnDirectorySelector
            // 
            btnDirectorySelector.Enabled = false;
            btnDirectorySelector.Location = new Point(26, 26);
            btnDirectorySelector.Name = "btnDirectorySelector";
            btnDirectorySelector.Size = new Size(168, 23);
            btnDirectorySelector.TabIndex = 2;
            btnDirectorySelector.Text = "Settings";
            btnDirectorySelector.UseVisualStyleBackColor = true;
            btnDirectorySelector.Click += btnDirectorySelector_Click;
            // 
            // lblSelectedPath
            // 
            lblSelectedPath.AutoSize = true;
            lblSelectedPath.Location = new Point(30, 55);
            lblSelectedPath.Name = "lblSelectedPath";
            lblSelectedPath.Size = new Size(0, 15);
            lblSelectedPath.TabIndex = 3;
            // 
            // progressBar1
            // 
            progressBar1.Enabled = false;
            progressBar1.Location = new Point(26, 376);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(588, 12);
            progressBar1.Step = 20;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 4;
            progressBar1.Value = 25;
            progressBar1.Visible = false;
            // 
            // lblTerraformVersion
            // 
            lblTerraformVersion.Anchor = AnchorStyles.Top;
            lblTerraformVersion.AutoSize = true;
            lblTerraformVersion.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblTerraformVersion.Location = new Point(397, 30);
            lblTerraformVersion.Name = "lblTerraformVersion";
            lblTerraformVersion.Size = new Size(174, 15);
            lblTerraformVersion.TabIndex = 5;
            lblTerraformVersion.Text = "Terraform Version: Checking...";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(637, 403);
            Controls.Add(lblTerraformVersion);
            Controls.Add(progressBar1);
            Controls.Add(lblSelectedPath);
            Controls.Add(btnDirectorySelector);
            Controls.Add(txtOutput);
            Controls.Add(btnRunTerraform);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Terraform Runner";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRunTerraform;
        private TextBox txtOutput;
        private Button btnDirectorySelector;
        private FolderBrowserDialog project_fbg;
        private Label lblSelectedPath;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private Label lblTerraformVersion;
    }
}