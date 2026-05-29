using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Models
{
	internal static class LawyerConverter
	{
		public static LawyerDto ConvertToDto(this Lawyer lawyer)
		{
			return new LawyerDto
			{
				LawyerId = lawyer.LawyerId,
				FirstName = lawyer.FirstName,
				LastName = lawyer.LastName,
				Email = lawyer.Email
			};
		}
	}
}
