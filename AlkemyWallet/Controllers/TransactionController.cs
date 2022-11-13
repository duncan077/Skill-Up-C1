using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AlkemyWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Regular")]
        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery]int page)
        {
            try
            {
                var result =  await _transactionService.getAll(page, User.Identity.Name);
               var response =_mapper.Map<List<TransactionDTO>>(result.ToList());
                if (response.Count>0)
                {
                    string NextUrl = string.Empty;
                    string PreviousUrl = string.Empty;
                    string ActionPath = Request.Host + Request.Path;
                    NextUrl = result.HasNext ? $"Next Page: {ActionPath} /page= {(page + 1)}" : string.Empty;
                    PreviousUrl = result.HasPrevious ? $"Previous Page: {ActionPath} /page= {(page - 1)}" : string.Empty;
                    return Ok(new {result,
                        PreviousURL = PreviousUrl,
                        NextURl=NextUrl

                    });
                }
                return NotFound(new { Status = "404", Message = "Error: Not found" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Status = "400", Message = $"Error: {ex.Message}" });
            }
           
        }

        [HttpPost]
        [Authorize(Roles ="Regular")]
        public async Task<IActionResult> CreateTransaction([FromBody]TransactionEntity transaction)
        {

            try
            {
                await CreateTransaction(_mapper.Map<TransactionEntity>(transaction));
                return Accepted(new { Status = "202", Message = $"Accepted" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Status = "400", Message = $"Error: {ex.Message}" });
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {

            try
            {
                await _transactionService.DeleteTransaction(id);
                return Accepted(new { Status = "202", Message = $"Deleted" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Status = "", Message = $"Error: {ex.Message}" });
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTransaction([FromBody] TransacctionUpdateDTO transaction, int id)
        {

            try
            {
                if(transaction.Id != id)
                    return BadRequest(new { Status = "400", Message = "Error: Transaction Id and Id requested are not equal" });
                await _transactionService.UpdateTransaction(_mapper.Map<TransactionEntity>(transaction),id);
                return Accepted(new { Status = "202", Message = "Transaction Updated" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Status = "400", Message = $"Error: {ex.Message}" });
            }
        }

        [Authorize(Roles = "Regular")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionEntity>> GetTransactionById(int id)
        {
            try {
                var transactionDetail = await _transactionService.getById(id);
                if(transactionDetail is null)
                    return NotFound(new { Status="Not Found", Message = "Trnasaction Not Found"});
                return Ok(transactionDetail);
            } catch (Exception ex)
            { 
                return BadRequest(new {Status ="", Message =$"Error: {ex.Message}" });
            }
            
        }

    }
}
