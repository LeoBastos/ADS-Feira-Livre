using Microsoft.Extensions.Configuration;

namespace ads.feira.api.Helpers.S3BucketConfig
{
    public class AWSConfiguration
    {
        public string AWSRegion { get; set; }
        public string AWSProfileName { get; set; }
        public S3Configuration S3Configuration { get; set; }
    }
}
