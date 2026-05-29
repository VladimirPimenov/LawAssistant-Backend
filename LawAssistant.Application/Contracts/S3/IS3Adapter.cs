using Microsoft.AspNetCore.Http;

namespace LawAssistant.Application.Contracts
{
    /// <summary>
    /// Клиент API S3 хранилища
    /// </summary>
    public interface IS3Adapter
    {
        /// <summary>
        /// Возвращает файл по ключу
        /// </summary>
        /// <param name="key">Ключ файла</param>
        /// <returns>HTTP-форма файла</returns>
        public Task<IFormFile> GetObjectAsync(string key);

        /// <summary>
        /// Сохраняет файл в хранилище
        /// </summary>
        /// <param name="file">HTTP-форма файла</param>
        /// <param name="key">Ключ сохраняемого файла</param>
        public Task PutObjectAsync(IFormFile file, string key);
    }
}
