namespace ads.feira.api.Helpers.S3BucketConfig
{
    public class S3Configuration
    {
        public string BucketName { get; set; }
        public string CloudFrontDomain { get; set; }
        public long MaxFileSize { get; set; }
        public string[] AllowedFileTypes { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailPath { get; set; }
        public int PresignedUrlExpirationMinutes { get; set; }
    }
}
