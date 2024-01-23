using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace TerraformRunner
{
    public class Instance
    {
        public static async Task<string> RunInstances(string imageId, 
            string instanceType, string groupId, string instanceName)
        {

            var client = new AmazonEC2Client(MainWindow.awsKeyId, MainWindow.awsKeySecret, RegionEndpoint.USEast1);

            var runInstanceRequest = new RunInstancesRequest
            {
                ImageId = imageId, // Replace with a valid AMI ID
                InstanceType = instanceType, // Specify the instance type
                MinCount = 1,
                MaxCount = 1,
                IamInstanceProfile = new IamInstanceProfileSpecification
                {
                    Name = "ec2_iam_fullaccess" // Replace with your IAM role name
                },
                SecurityGroupIds = new List<string> { groupId },
                TagSpecifications = new List<TagSpecification>
                {
                    new TagSpecification
                    {
                        ResourceType = Amazon.EC2.ResourceType.Instance,
                        Tags = new List<Tag>
                        {
                            new Tag
                            {
                                Key = "Name",
                                Value = instanceName
                            }
                        }
                    }
                }
            };

            try
            {
                var response = await client.RunInstancesAsync(runInstanceRequest);
                foreach (var instance in response.Reservation.Instances)
                {
                    Console.WriteLine("Created instance with ID: " + instance.InstanceId);

                }
                return response.Reservation.Instances[0].InstanceId;
            }
            catch (AmazonEC2Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
