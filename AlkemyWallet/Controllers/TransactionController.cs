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
using NSwag.Annotations;

namespace AlkemyWallet.Controllers
{
    [OpenApiTag("Transaction",
                Description = "Web API para CRUD de Transacciones"
                )]
    [Route("[controller]")]
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
        // GET: Transaction
        /// <summary>
        /// Obtiene un listado de transacciones
        /// </summary>
        /// <remarks>
        /// Obtiene un listado de transacciones realizadas por el usuario
        /// </remarks>
        
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>        
       
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
                    return Ok(new {
                        response,
                        PreviousURL = PreviousUrl,
                        NextURl=NextUrl

                    });
                }
                return NotFound(new { Status = "404", Message = "Error: Not found" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Status = "Bad Request", Message = $"Error: {ex.Message}" });
            }
           
        }
        // POST: Transaction
        /// <summary>
        /// Crea el registro de la transaccion
        /// </summary>
        /// <remarks>
        /// Crea el registro de la transaccion
        /// </remarks>
       
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Creado con exito.</response>        
        /// <response code="400">Bad Request. No se ha podido validar la transaccion.</response>        

        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        [Authorize(Roles ="Regular")]
        public async Task<IActionResult> CreateTransaction([FromBody]TransactionDTO transaction)
        {

            try
            {
                await _transactionService.CreateTransaction(_mapper.Map<TransactionEntity>(transaction));
                return Accepted(new { Status = "Created", Message = "Transaction created successfuly" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Status = "Bad Request", Message = $"Error: {ex.Message}" });
            }
        }
        // DELETE: Transaction/5
        /// <summary>
        /// Elimina el registro de la transaccion del id proporcionado
        /// </summary>
        /// <remarks>
        /// Elimina el registro de la transaccion del id proporcionado
        /// </remarks>
        /// <param name="id">Id de la Transaccion a eliminar</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Elimado con exito.</response>        
        /// <response code="400">Bad Request. No se ha podido validar la transaccion.</response>        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {

            try
            {
                await _transactionService.DeleteTransaction(id);
                return Accepted(new { Status = "Transaction Deleted", Message = $"Transaction {id} deleted successfuly" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Status = "Bad request", Message = $"Error: {ex.Message}" });
            }
        }

        // PUT: Transaction/5
        /// <summary>
        /// Actualiza el registro de la transaccion del id proporcionado
        /// </summary>
        /// <remarks>
        /// Actualiza el registro de la transaccion del id proporcionado
        /// </remarks>
        /// <param name="transaction">Transaccion con los datos actualizados</param>
        /// <param name="id">Id de la transaccion a actualizar</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="202">ACcepted. Actualizado con exito.</response>        
        /// <response code="400">Bad Request. No se ha podido validar la transaccion.</response>        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTransaction([FromBody] TransactionDTO transaction, int id)
        {

            try
            {
                if(transaction.Id != id)
                    return BadRequest(new { Status = "Bad Request", Message = "Error: Transaction Id and Id requested are not equal" });
                await _transactionService.UpdateTransaction(_mapper.Map<TransactionEntity>(transaction),id);
                return Accepted(new { Status = "Transaction Updated", Message = $"Transaction {id} Updated" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Status = "Bad Request", Message = $"Error: {ex.Message}" });
            }
        }
        // GET: Transaction/5
        /// <summary>
        /// Devuelve el registro de la transaccion del id proporcionado
        /// </summary>
        /// <remarks>
        /// Devuelve el registro de la transaccion del id proporcionado
        /// </remarks>

        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve la transaccion pedida.</response>        
        /// <response code="400">Bad Request. No se ha podido validar la transaccion.</response>        
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
