using Microsoft.AspNetCore.Http;

namespace LawAssistant.Application.Contracts
{
    public interface IS3Adapter
    {
        public Task<IFormFile> GetObjectAsync(string key);

        public Task PutObjectAsync(IFormFile file, string key);
    }
}
