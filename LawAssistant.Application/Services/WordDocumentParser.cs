using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

using LawAssistant.Application.Contracts;

namespace LawAssistant.Application.Services
{
    public class WordDocumentParser : IDocumentParser
    {
        public List<string> ParseDocumentIntoParagraphs(string filePath)
        {
            var paragraphs = new List<string>();

            using (var document = WordprocessingDocument.Open(filePath, false)) 
            {
                var documentBody = document.MainDocumentPart.Document.Body;

                foreach (var paragraph in documentBody.Elements<Paragraph>())
                {
                    string text = paragraph.InnerText;
                    paragraphs.Add(text);
                }
            };

            return paragraphs;
        }
    }
}
