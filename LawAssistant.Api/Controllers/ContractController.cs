using Microsoft.AspNetCore.Mvc;

using LawAssistant.Application.Models;
using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с коллективными договорами.
    /// </summary>
    [ApiController, Route("contracts")]
    public class ContractController(
        IContractService contractService,
        IContractFileService contractFileService,
        IReportService reportService)
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
        //[Authorize]
        [HttpGet]
        [ProducesResponseType<CollectiveContract>(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetContractAsync([FromQuery] int contractId)
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
        //[Authorize]
        [HttpGet("{contractId}/file")]
        public async Task<IActionResult> GetContractFileAsync([FromRoute] int contractId)
        {
            var file = await contractFileService.LoadContractFileAsync(contractId);

            if(file == null)
                return NotFound();

			var stream = file.OpenReadStream();

            string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            string fileName = $"contract-{contractId}";

			return File(stream, contentType, fileName);
		}

        /// <summary>
        /// Создаёт договор.
        /// </summary>
        /// <param name="contractRequest">Запрос на создание договора.</param>
        /// <returns>
        /// 200 (Ok) с созданным договором.
        /// 400 (BadRequest) при ошибке.
        /// </returns>
        //[Authorize]
        [HttpPost]
		[ProducesResponseType<CollectiveContract>(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreateContractAsync([FromForm] CreateContractRequest contractRequest)
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
		//[Authorize]
        [HttpPut]
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
		//[Authorize]
        [HttpDelete]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> DeleteContractAsync([FromQuery] int contractId)
        {
            var removedContractId = await contractService.RemoveContractAsync(contractId);

            return removedContractId == null ? BadRequest() : Ok();
        }
        
        /// <summary>
        /// Получает отчёты для договора
        /// </summary>
        /// <param name="contractId">Идентификатор договора</param>
        /// <returns>Список отчётов</returns>
        //[Authorize]
        [HttpGet("{contractId}/reports")]
		[ProducesResponseType<ComparisonReport>(200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetContractReportsAsync([FromRoute] int contractId)
		{
			var reports = await reportService.GetContractReportsAsync(contractId);

			return reports == null ? NotFound() : Ok(reports);
		}
    }
}
