using Microsoft.AspNetCore.Http;

namespace LawAssistant.Infrastructure.S3
{
	internal class FormFile : IFormFile
    {
        public string Name { get; }

        public string FileName { get; }

        public long Length => _stream.Length;

        public string ContentType { get; set; }

        public string ContentDisposition { get; set; }

        public IHeaderDictionary Headers { get; }

        private readonly Stream _stream;

        public FormFile(Stream stream, string name, string fileName, string contentType)
        {
            _stream = stream;
            Name = name;
            FileName = fileName;
            ContentType = contentType;
            ContentDisposition = string.Empty;
            Headers = new HeaderDictionary();
        }

        public void CopyTo(Stream target) => _stream.CopyTo(target);

        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default) =>
            await _stream.CopyToAsync(target, cancellationToken);

        public Stream OpenReadStream() => _stream;
    }
}
