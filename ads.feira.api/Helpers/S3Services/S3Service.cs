using ads.feira.api.Helpers.S3BucketConfig;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Options;

namespace ads.feira.api.Helpers.S3Services
{    
    public class S3Service
    {
        private readonly AWSConfiguration _awsConfig;
        private readonly IAmazonS3 _s3Client;

        public S3Service(IOptions<AWSConfiguration> awsConfig, IAmazonS3 s3Client)
        {
            _awsConfig = awsConfig.Value;
            _s3Client = s3Client;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var key = $"{_awsConfig.S3Configuration.ImagePath}{fileName}";

            using (var stream = file.OpenReadStream())
            {
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = stream,
                    Key = key,
                    BucketName = _awsConfig.S3Configuration.BucketName,
                    ContentType = file.ContentType
                };

                var fileTransferUtility = new TransferUtility(_s3Client);
                await fileTransferUtility.UploadAsync(uploadRequest);
            }

            return $"{_awsConfig.S3Configuration.CloudFrontDomain}/{key}";
        }
    }
}
