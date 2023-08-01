using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using TransactionsTestTask.BLL.Enums;
using TransactionsTestTask.BLL.Helpers;
using TransactionsTestTask.BLL.Models;
using TransactionsTestTask.BLL.ServiceErrors;
using TransactionsTestTask.BLL.Services.Contracts;
using TransactionsTestTask.DAL.Entities;

namespace TransactionsTestTask.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Import data from excel file to save it into a database
        /// </summary>
        /// <param name="file">The excel document</param>
        /// <returns>Returns success or an error response on failure</returns>
        /// <response code="400">Returns an error response when the import fails</response>
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

        /// <summary>
        /// Export data from database into a csv file
        /// </summary>
        /// <param name="transactionParameters">Parameters to filter transactions by</param>
        /// <returns>Returns csv file in response body to download or an error response on failure</returns>
        /// <response code="400">Returns an error response when the export fails</response>
        [HttpGet]
        [Route("export")]
        public IActionResult ExportToCsv([FromQuery] TransactionQueryParameters transactionParameters)
        {
            var exportResult = _transactionService.ExportToCsv(transactionParameters);
            if (!exportResult.Succeeded)
            {
                return BadRequest(new ApiError(400, exportResult.Errors));
            }

            return File(exportResult.Value!, "text/csv", "transactions.csv");
        }

        /// <summary>
        /// Get all transactions from database
        /// </summary>
        /// <param name="transactionParameters">Parameters to filter transactions by</param>
        /// <returns>Returns list of transactions filtered by parameters</returns>
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

        /// <summary>
        /// Update transaction status
        /// </summary>
        /// <param name="transactionId">The ID of a transaction is to be updated.</param>
        /// <param name="status">New status of the transaction.</param>
        /// <returns>Returns success or an error response on failure</returns>
        /// <response code="400">Returns an error response when the update fails</response>
        [HttpPatch]
        [Route("update-status/{transactionId}")]
        public async Task<IActionResult> UpdateStatus(int transactionId, TransactionStatus? status)
        {
            if (status == null)
            {
                return BadRequest(new ApiError(400, TransactionServiceErrors.UPDATE_FAILED_INVALID_STATUS));
            }

            var updateResult = await _transactionService.UpdateStatusAsync(transactionId, status);
            if (!updateResult.Succeeded)
            {
                return BadRequest(new ApiError(400, updateResult.Errors));
            }

            return Ok();
        }
    }
}
