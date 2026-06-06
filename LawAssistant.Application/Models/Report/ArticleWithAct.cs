using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Models
{
    public record ArticleWithAct
    {
        public int ActId { get; init; }
        
        public string ActTitle { get; init; }
        
        public ActArticle Article { get; init; }
        
    }
}