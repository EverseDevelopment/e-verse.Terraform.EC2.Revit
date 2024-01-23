using Amazon.EC2.Model;
using Amazon.EC2;
using System.Windows.Controls;
using Amazon;

namespace TerraformRunner.ec2Elements
{
    public static class Amis
    {
        public static async Task LoadAmisAsync(ec2ViewModel ec2View, string awsKeyId, string awsKeySecret)
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
                            foreach (var image in response.Images)
                            {
                                AMIObject aMIObject = new AMIObject(image.ImageId, image.Name);
                                ec2View.Amis.Add(aMIObject);
                            }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
