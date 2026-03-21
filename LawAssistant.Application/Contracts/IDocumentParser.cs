using Microsoft.AspNetCore.Http;

namespace LawAssistant.Application.Contracts
{
    public interface IDocumentParser
    {
        public List<string> ParseDocumentIntoParagraphs(string filePath);
    }
}
