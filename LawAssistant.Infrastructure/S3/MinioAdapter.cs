using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using Minio;

using LawAssistant.Application.Contracts;
using LawAssistant.Infrastructure.Settings;

namespace LawAssistant.Infrastructure.S3
{
    internal class S3Adapter : IS3Adapter
    {
        private readonly S3Configuration s3Config;
        private readonly MinioClient minioClient;
        
        public S3Adapter(IConfiguration config)
        {
            s3Config = config.GetSection(nameof(S3Configuration)).Get<S3Configuration>();
            
            minioClient = new MinioClient()
                .WithEndpoint(s3Config.Url)
                .WithCredentials(s3Config.Login, s3Config.Password)
                .WithSSL(s3Config.UseSsl)
                .Build();
        }

        public async Task<IFormFile> GetObjectAsync(string key)
        {
            var memoryStream = new MemoryStream();
        
            var getObjectArgs = new GetObjectArgs()
                .WithObject(key)
                .WithBucket(s3Config.DocumentsBucketName)
                .WithCallbackStream(stream => stream.CopyToAsync(memoryStream));
                
            await minioClient.GetObjectAsync(getObjectArgs);
            
            memoryStream.Position = 0;
            
            IFormFile file = new FormFile(memoryStream, key, key, "contract");
            return file;
        }

        public async Task PutObjectAsync(IFormFile file, string key)
        {
            await CheckBucketExistsAsync(s3Config.DocumentsBucketName);
            
            var putObjectArgs = new PutObjectArgs()
                .WithObject(key)
                .WithBucket(s3Config.DocumentsBucketName)
                .WithStreamData(file.OpenReadStream())
                .WithObjectSize(file.Length);
                
            await minioClient.PutObjectAsync(putObjectArgs);
        }
    
        public async Task DeleteObjectAsync(string key)
        {
            var deleteObjectArgs = new RemoveObjectArgs()
                .WithObject(key)
                .WithBucket(s3Config.DocumentsBucketName);
                
            await minioClient.RemoveObjectAsync(deleteObjectArgs);
        }
        
        private async Task CheckBucketExistsAsync(string bucketName)
        {
            var checkBucketArgs = new BucketExistsArgs()
                .WithBucket(bucketName);
                
            var isBucketExists = await minioClient.BucketExistsAsync(checkBucketArgs);
            
            if (!isBucketExists)
            {
                var createBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);
                await minioClient.MakeBucketAsync(createBucketArgs);
            }
        }
    }
}