using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LawAssistant.Application.Models;
using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;
using LawAssistant.Application.Contracts.S3;

namespace LawAssistant.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с коллективными договорами.
    /// </summary>
    [ApiController, Route("contract")]
    public class ContractController(
        IContractService contractService,
        IContractFileService contractFileService)
        : ControllerBase
    {
        /// <summary>
        /// Получает договор по идентификатору.
        /// </summary>
        /// <param name="contractId">Идентификатор договора</param>
        /// <returns>
        /// 200 (Ок) с найденным договором.
        /// 404 (NotFound) если договор не найден.
        /// </returns>
        [Authorize]
        [HttpGet("get-contract")]
        [ProducesResponseType<CollectiveContract>(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetContractAsync(int contractId)
        {
            var contract = await contractService.GetContractAsync(contractId);

            return contract == null ? NotFound() : Ok(contract);
        }

        /// <summary>
        /// Получает файл договора по его идентификатору.
        /// </summary>
        /// <param name="contactId">Идентификатор договора</param>
        /// <returns>
        /// Файл договора.
        /// 404 (NotFound), если договор/файл не найден.
        /// </returns>
        [Authorize]
        [HttpGet("get-contract-file")]
        public async Task<IActionResult> GetContractFileAsync(int contactId)
        {
            var file = await contractFileService.LoadContractFileAsync(contactId);

            if(file == null)
                return NotFound();

			var stream = file.OpenReadStream();

            string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            string fileName = $"contract-{contactId}";

			return File(stream, contentType, fileName);
		}

        /// <summary>
        /// Получает договоры юриста по его идентификатору.
        /// </summary>
        /// <param name="lawyerId">Идентификатор юриста</param>
        /// <returns>
        /// 200 (Ok) со списком договоров.
        /// 404 (NotFound) если договоры не найдены.
        /// </returns>
        [Authorize]
        [HttpGet("get-lawyer-contracts")]
        [ProducesResponseType<List<ContractDto>>(200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetLawyerContracts(int lawyerId)
        {
            var lawyerContracts = await contractService.GetLawyerContractsInfoAsync(lawyerId);

            return lawyerContracts == null || lawyerContracts.Count == 0
                ? NotFound() : Ok(lawyerContracts);
        }

        /// <summary>
        /// Создаёт договор.
        /// </summary>
        /// <param name="contractRequest">Запрос на создание договора.</param>
        /// <returns>
        /// 200 (Ok) с созданным договором.
        /// 400 (BadRequest) при ошибке.
        /// </returns>
        [Authorize]
        [HttpPost("create-contract")]
		[ProducesResponseType<CollectiveContract>(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreateContractAsync(CreateContractRequest contractRequest)
        {
            var createdContract = await contractService.CreateContractAsync(contractRequest);

            return createdContract == null ? BadRequest() : Ok(createdContract);
        }

		/// <summary>
		/// Изменяет существующий договор.
		/// </summary>
		/// <param name="contractDto">Изменённый договор.</param>
		/// <returns>
		/// 200 (Ok) с измённым договором.
		/// 400 (BadRequest) при ошибке.
		/// </returns>
		[Authorize]
        [HttpPut("update-contract")]
		[ProducesResponseType<CollectiveContract>(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> UpdateContractAsync(ContractDto contractDto)
        {
            var updatedContract = await contractService.UpdateContractAsync(contractDto);

            return updatedContract == null ? BadRequest() : Ok(updatedContract);
        }

		/// <summary>
		/// Удаляет существующий договор.
		/// </summary>
		/// <param name="contractId">Идентификатор договора.</param>
		/// <returns>
		/// 200 (Ok) при успешном удалении.
		/// 400 (BadRequest) при ошибке.
		/// </returns>
		[Authorize]
        [HttpDelete("delete-contract")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> DeleteContractAsync(int contractId)
        {
            var removedContractId = await contractService.RemoveContractAsync(contractId);

            return removedContractId == null ? BadRequest() : Ok();
        }
    }
}
