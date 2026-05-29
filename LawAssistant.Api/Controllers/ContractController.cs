using Microsoft.AspNetCore.Mvc;

using LawAssistant.Application.Models;
using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с коллективными договорами
    /// </summary>
    [ApiController, Route("contracts")]
    public class ContractController(
        IContractService contractService,
        IContractFileService contractFileService,
        IReportService reportService)
        : ControllerBase
    {
        /// <summary>
        /// Возвращает договор по идентификатору.
        /// </summary>
        /// <param name="contractId">Идентификатор договора</param>
        /// <returns>Найденный договор</returns>
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
        /// Возвращает файл договора по его идентификатору
        /// </summary>
        /// <param name="contactId">Идентификатор договора</param>
        /// <returns>Файл договора</returns>
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
        /// Создаёт договор
        /// </summary>
        /// <param name="contractRequest">Запрос на создание договора</param>
        /// <returns>Созданный договор</returns>
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
		/// Изменяет данные существующего договора
		/// </summary>
		/// <param name="contractDto">Договор с обновлёнными полями</param>
		/// <returns>Изменённый договор</returns>
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
		/// Удаляет существующий договор
		/// </summary>
		/// <param name="contractId">Идентификатор договора</param>
		/// <returns>Идентификатор удалённого договора</returns>
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
        /// Возвращает отчёты для договора
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
