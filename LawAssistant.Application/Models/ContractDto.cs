using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
namespace LawAssistant.Application.Models
{
    public record ContractDto
    {
        public string Title { get; init; }

        public IFormFile ContractFile { get; init; }
    }
}
