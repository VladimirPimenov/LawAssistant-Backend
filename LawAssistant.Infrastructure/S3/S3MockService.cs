using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using LawAssistant.Infrastructure.Settings;
using LawAssistant.Application.Contracts;

namespace LawAssistant.Infrastructure.S3
{
	internal class S3MockService : IS3Adapter
    {
        private readonly S3Configuration s3Config;

        public S3MockService(IConfiguration config)
        {
            s3Config = config.GetSection(nameof(S3Configuration)).Get<S3Configuration>();
        }

        public async Task<IFormFile> GetObjectAsync(string key)
        {
            string filePath = s3Config.Endpoint + key + ".docx";

            var memoryStream = new MemoryStream();
            await using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                await fileStream.CopyToAsync(memoryStream);
            }

            memoryStream.Position = 0;

            IFormFile file = new FormFile(memoryStream, key, key, "contract");

            return file;
        }

        public async Task PutObjectAsync(IFormFile file, string key)
        {
            string filePath = s3Config.Endpoint + key + ".docx";

            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }
}
