using System;
using System.Threading.Tasks;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon;

namespace TerraformRunner
{
    public static class SecurityGroups
    {
        public static async Task Get(ec2ViewModel ec2View, string awsKeyId, string awsKeySecret)
        {
            try
            {
                using (var ec2Client = new AmazonEC2Client(awsKeyId, awsKeySecret, RegionEndpoint.USEast1))
                {
                    var response = await ec2Client.DescribeSecurityGroupsAsync(new DescribeSecurityGroupsRequest());

                    foreach (var group in response.SecurityGroups)
                    {
                            SecurityObject securityObject = new SecurityObject(group.GroupId, group.GroupName);
                            ec2View.Securities.Add(securityObject);
                    }
                }
            }
            catch (AmazonEC2Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }
    }

}


