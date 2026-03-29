using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LawAssistant.Application.Models;
using LawAssistant.Application.Contracts;

namespace LawAssistant.Api.Controllers
{
    [ApiController, Route("contract")]
    public class ContractController(
        IContractService contractService)
        : ControllerBase
    {
        [Authorize]
        [HttpGet("get-contract")]
        public async Task<IActionResult> GetContractAsync(int contractId)
        {
            var contract = await contractService.GetContractAsync(contractId);

            return contract == null ? NotFound() : Ok(contract);
        }

        [Authorize]
        [HttpGet("get-contract-file")]
        public async Task<IActionResult> GetContractFileAsync(int contactId)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpGet("get-lawyer-contracts")] 
        public async Task<IActionResult> GetLawyerContracts(int lawyerId)
        {
            var lawyerContracts = await contractService.GetLawyerContractsInfoAsync(lawyerId);

            return lawyerContracts == null || lawyerContracts.Count == 0
                ? NotFound() : Ok(lawyerContracts);
        }

        [Authorize]
        [HttpPost("create-contract")]
        public async Task<IActionResult> CreateContractAsync(CreateContractRequest contractRequest)
        {
            var createdContract = await contractService.CreateContractAsync(contractRequest);

            return createdContract == null ? BadRequest() : Ok(createdContract);
        }

        [Authorize]
        [HttpPut("update-contract")]
        public async Task<IActionResult> UpdateContractAsync(ContractDto contractDto)
        {
            var updatedContract = await contractService.UpdateContractAsync(contractDto);

            return updatedContract == null ? BadRequest() : Ok(updatedContract);
        }

        [Authorize]
        [HttpDelete("delete-contract")]
        public async Task<IActionResult> DeleteContractAsync(int contractId)
        {
            var removedContractId = await contractService.RemoveContractAsync(contractId);

            return removedContractId == null ? BadRequest() : Ok();
        }
    }
}
