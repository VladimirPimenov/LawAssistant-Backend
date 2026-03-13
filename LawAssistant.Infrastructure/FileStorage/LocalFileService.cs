using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using LawAssistant.Application.Contracts;
using LawAssistant.Api.Settings;

namespace LawAssistant.Infrastructure.FileStorage
{
    public class LocalFileService : IFileService
    {
        private readonly FileServerConfiguration fileServerConfig;

        public LocalFileService(IConfiguration config)
        {
			fileServerConfig = config
				.GetSection(nameof(FileServerConfiguration))
				.Get<FileServerConfiguration>();
		}

        public async Task LoadFileToServer(IFormFile file)
        {
            string filePath = fileServerConfig.Path + file.FileName;

            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public async Task<IFormFile> LoadFileFromServer(string fileName)
        {
            string filePath = fileServerConfig.Path + fileName;

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
