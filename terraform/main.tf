provider "aws" {
  region = "us-east-1"  # You can choose your preferred region
}

data "aws_security_group" "existing_sg" {
  name = "RevitTestInstance"  # Replace with the actual name of your security group
}

data "aws_iam_role" "existing_role" {
  name = "ec2_iam_fullaccess"  # Replace with the actual name of your IAM role
}

resource "aws_instance" "example" {
  ami           = "Change this to your desired AMI"  # AMI ID of a windows server 2022 instance and Revit 2024 installed
  instance_type = "t3.xlarge"    # Smaller instances than this cannot run Revit properly

   root_block_device {
    volume_type = "gp2"  # General Purpose SSD
    volume_size = 100     # Size in GiB
  }

  iam_instance_profile = data.aws_iam_role.existing_role.name

  vpc_security_group_ids = [data.aws_security_group.existing_sg.id]

  key_name = "RevitTestInstance"  # Use this Keypair as default

  tags = {
  Name = "RevitTestInstance"
  }
  # Other required configurations like security groups, key pairs, etc.
}