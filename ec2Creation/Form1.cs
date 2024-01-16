using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace TerraformRunner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Set up event handlers for the BackgroundWorker
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.WorkerReportsProgress = true;

            backgroundWorker2.DoWork += backgroundWorker2_DoWork;
            backgroundWorker2.RunWorkerCompleted += backgroundWorker2_RunWorkerCompleted;
            backgroundWorker2.RunWorkerAsync();
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            // Run the Terraform version command
            string versionOutput = RunTerraformVersionCommand("version", worker);

            // Pass the version output to the RunWorkerCompleted event
            e.Result = versionOutput;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // Handle the completion of the background operation
            if (e.Error != null)
            {
                MessageBox.Show("Error checking Terraform version: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Display the Terraform version in the label
                lblTerraformVersion.Text = e.Result.ToString().Split("\n")[0];
            }
        }

        private string RunTerraformVersionCommand(string command, BackgroundWorker worker)
        {
            try
            {
                // Set up process start info
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "terraform",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Start the process
                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();

                    // Read standard output
                    string output = process.StandardOutput.ReadToEnd();

                    // Capture and display error output
                    string error = process.StandardError.ReadToEnd();
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show("Error: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    process.WaitForExit();

                    return output;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error running Terraform command: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        private void btnRunTerraform_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(lblSelectedPath.Text))
            //{
            //    MessageBox.Show("Please select a directory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            // Start the BackgroundWorker
            backgroundWorker1.RunWorkerAsync();
        }

        private void RunTerraformCommand(string command, BackgroundWorker worker)
        {
            try
            {
                worker.ReportProgress(0);
                // Set up process start info
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "terraform",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Start the process
                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.OutputDataReceived += (s, e) =>
                    {
                        if (e.Data != null)
                            UpdateTextBox(e.Data.Replace("\n", Environment.NewLine));
                    };
                    process.ErrorDataReceived += (s, e) =>
                    {
                        if (e.Data != null)
                            UpdateTextBox(e.Data.Replace("\n", Environment.NewLine));
                    };
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();




                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error running Terraform command: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTextBox(string value)
        {
            if (txtOutput.InvokeRequired)
            {
                // If called from a different thread, invoke the method on the UI thread
                txtOutput.Invoke(new Action<string>(UpdateTextBox), value);
            }
            else
            {
                // Update the TextBox value
                txtOutput.AppendText(value + Environment.NewLine);
            }
        }

        private void btnDirectorySelector_Click(object sender, EventArgs e)
        {
            if (project_fbg.ShowDialog() == DialogResult.OK)
            {
                lblSelectedPath.Text = project_fbg.SelectedPath;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            worker.ReportProgress(0);

            Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            // Run the Terraform command
            RunTerraformCommand("init", worker);
            RunTerraformCommand("apply -auto-approve", worker);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update the progress bar
            progressBar1.Enabled = true;
            progressBar1.Visible = true;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Perform any post-processing after the background operation completes
            progressBar1.Enabled = false;
            progressBar1.Visible = false;
            if (e.Error != null)
            {
                MessageBox.Show("Error running Terraform command: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Terraform command completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}