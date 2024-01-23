using Amazon.EC2;
using Amazon.EC2.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Amazon;
using static System.Net.Mime.MediaTypeNames;
using TerraformRunner.ec2Elements;

namespace TerraformRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string awsKeyId;
        public static string awsKeySecret;
        
        public MainWindow()
        {

            awsKeyId = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            awsKeySecret = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");

            if (awsKeyId == null || awsKeySecret == null)
            {
                MessageWindow.Show("Warning", "Make sure to create your AWS Keys as enviroment variables");
                this.Close();
            }
            else
            {
                this.ec2ViewModel = new ec2ViewModel();
                this.DataContext = this.ec2ViewModel;

                AMIObject aMIObject = new AMIObject("ami-00d990e7e5ece7974", "Microsoft Windows Server 2022 Base");
                ec2ViewModel.Amis.Add(aMIObject);

                InitializeComponent();
                Amis.LoadAmisAsync(ec2ViewModel, awsKeyId, awsKeySecret);
                SecurityGroups.Get(ec2ViewModel, awsKeyId, awsKeySecret);
            }
        }

        private ec2ViewModel ec2ViewModel { get; set; }
        private async void CreateButton(object sender, RoutedEventArgs e)
        {
            if (Validation.formRun(InstanceNameTextBox.Text, "Instance Name"))
            {
                return;
            }

            if (Validation.formRun(comboBoxGroups.Text, "GroupId"))
            {
                return;
            }

            if (Validation.formRun(comboBoxAmis.Text, "AMI"))
            {
                return;
            }

            try
            {
                AMIObject amiImage = (AMIObject)comboBoxAmis.SelectedItem;
                SecurityObject securityObject = (SecurityObject)comboBoxGroups.SelectedItem;

                var response = await Instance.RunInstances(amiImage.ImageId, 
                    InstanceType.T3aXlarge, securityObject.SecurityId, InstanceNameTextBox.Text);

                MessageWindow.Show("Success", response);

            }
            catch (AmazonEC2Exception ex)
            {
                MessageWindow.Show("Failuer", ex.Message);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        static async Task LoadAmisAsync(ComboBox comboBoxAmis, ec2ViewModel ec2View)
        {
            using (var ec2Client = new AmazonEC2Client(awsKeyId, awsKeySecret, RegionEndpoint.USEast1))
            {
                var request = new DescribeImagesRequest
                {
                    Owners = new List<string> { "self" } // or use specific AWS account IDs
                };

                try
                {
                    DescribeImagesResponse response = await ec2Client.DescribeImagesAsync(request);

                    if (response.Images != null)
                    {
                        comboBoxAmis.Dispatcher.Invoke(() =>
                        {
                            foreach (var image in response.Images)
                            {
                                AMIObject aMIObject = new AMIObject(image.ImageId, image.Name);
                                ec2View.Amis.Add(aMIObject);
                            }
                        });
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private void Title_Link(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.e-verse.com");
        }
    }
}