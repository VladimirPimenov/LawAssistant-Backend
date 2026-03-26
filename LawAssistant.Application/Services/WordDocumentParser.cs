using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;

using LawAssistant.Application.Contracts;

namespace LawAssistant.Application.Services
{
    public class WordDocumentParser : IDocumentParser
    {
        public List<string> ParseDocumentIntoParagraphs(IFormFile documentFile)
        {
            var paragraphs = new List<string>();

            using (var document = WordprocessingDocument.Open(documentFile.OpenReadStream(), false)) 
            {
                var documentBody = document.MainDocumentPart.Document.Body;

                foreach (var paragraph in documentBody.Elements<Paragraph>())
                {
                    string text = paragraph.InnerText;

                    if (String.IsNullOrEmpty(text) || String.IsNullOrWhiteSpace(text))
                        continue;
                    paragraphs.Add(text.Trim());
                }
            };

            return paragraphs;
        }
    }
}
