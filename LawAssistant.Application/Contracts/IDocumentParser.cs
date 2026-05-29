using Microsoft.AspNetCore.Http;

namespace LawAssistant.Application.Contracts
{
    /// <summary>
    /// Класс для деления документа на абзацы
    /// </summary>
    public interface IDocumentParser
    {
        /// <summary>
        /// Возвращает список абзацев документа
        /// </summary>
        /// <param name="documentFile">HTTP-форма документа</param>
        /// <returns>Список абзацев</returns>
        public List<string> ParseDocumentIntoParagraphs(IFormFile documentFile);
    }
}
