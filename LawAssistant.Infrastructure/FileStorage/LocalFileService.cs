using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using LawAssistant.Application.Contracts;

namespace LawAssistant.Infrastructure.FileStorage
{
    public class LocalFileService(
        IConfiguration config)
        : IFileService
    {
        private readonly string _storagePath = config["FileServerConfiguration:Path"];

        public async Task LoadFileToServer(IFormFile file)
        {
            string filePath = _storagePath + file.FileName;

            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public async Task<IFormFile> LoadFileFromServer(string fileName)
        {
            string filePath = _storagePath + fileName;

            var memoryStream = new MemoryStream();

            await using(var fileStream = new FileStream(filePath, FileMode.Open))
            {
                await fileStream.CopyToAsync(memoryStream);
            }

            memoryStream.Position = 0;

            IFormFile file = new FormFile(memoryStream, fileName, fileName, "contract");

            return file;
        }
    }
}
