using Microsoft.AspNetCore.Http;

namespace LawAssistant.Application.Contracts.S3
{
    public interface IS3Adapter
    {
        public Task<IFormFile> GetObjectAsync(string key);

        public Task PutObjectAsync(IFormFile file, string key);
    }
}
