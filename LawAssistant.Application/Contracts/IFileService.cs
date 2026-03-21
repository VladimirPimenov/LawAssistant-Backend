using Microsoft.AspNetCore.Http;

namespace LawAssistant.Application.Contracts
{
    public interface IFileService
    {
        public Task<string> LoadFileToServer(IFormFile file);

        public Task<IFormFile> LoadFileFromServer(string fileName);
    }
}
