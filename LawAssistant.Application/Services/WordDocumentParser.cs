using Microsoft.AspNetCore.Http;

using LawAssistant.Application.Contracts;

namespace LawAssistant.Application.Services
{
    public class WordDocumentParser : IDocumentParser
    {
        public List<string> ParseDocumentIntoParagraphs(string filePath)
        {
            var wordApp = new Microsoft.Office.Interop.Word.Application();
            var wordDocument = wordApp.Documents.Open(filePath);
            var wordParagraphs = wordDocument.Paragraphs;

            var paragraphs = new List<string>();

            foreach(Microsoft.Office.Interop.Word.Paragraph paragraph in wordParagraphs)
            {
                paragraphs.Add(paragraph.Range.Text);
            }

            wordDocument.Close();
            wordApp.Quit();

            return paragraphs;
        }
    }
}
