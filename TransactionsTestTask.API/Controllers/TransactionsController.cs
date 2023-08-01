﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using TransactionsTestTask.BLL.Enums;
using TransactionsTestTask.BLL.Helpers;
using TransactionsTestTask.BLL.Models;
using TransactionsTestTask.BLL.Services.Contracts;
using TransactionsTestTask.DAL.Entities;

namespace TransactionsTestTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [Route("import")]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            var importResult = await _transactionService.ImportFromExcel(file, User.FindFirst("id")?.Value);
            if (!importResult.Succeeded)
            {
                return BadRequest(new ApiError(400, importResult.Errors));
            }

            return Ok();
        }

        [HttpGet]
        [Route("export")]
        public async Task<IActionResult> ExportToCsv()
        {

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] TransactionQueryParameters transactionParameters)
        {
            var result = _transactionService.GetTransactions(transactionParameters);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiError(400, result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpPatch]
        [Route("update-status/{transactionId}")]
        public async Task<IActionResult> UpdateStatus(int transactionId)
        {

            return Ok();
        }
    }
}
