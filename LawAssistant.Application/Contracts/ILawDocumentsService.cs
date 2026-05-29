using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
	/// <summary>
	/// Сервис для работы с законодательными актами
	/// </summary>
	public interface ILawDocumentsService
	{
		/// <summary>
		/// Возвращает статью законодательного акта
		/// </summary>
		/// <param name="articleId">Идентификатор статьи</param>
		/// <returns>Статья</returns>
		public Task<ActArticle> GetArticleAsync(int articleId);

		/// <summary>
		/// Возвращает законодательный акт
		/// </summary>
		/// <param name="actId">Идентификатор акта</param>
		/// <returns>Акт</returns>
		public Task<LawAct> GetLawActAsync(int actId);
	}
}
