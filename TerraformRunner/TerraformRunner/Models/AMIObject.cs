namespace TerraformRunner
{
    public class AMIObject
    {
        public AMIObject(string imageId, string label)
        {
            ImageId = imageId;
            Label = label;
        }
        public string ImageId  { get; internal set; }
        public string Label { get; internal set; }
    }
}

