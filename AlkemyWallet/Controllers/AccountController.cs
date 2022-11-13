using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace AlkemyWallet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {



        private IAccountService _accountServices;
        private IMapper _mapper;

        public AccountController(IAccountService accountServices, IMapper mapper)
        {
            _mapper = mapper;
            _accountServices = accountServices;
        }


        // GET: Account?page
        /// <summary>
        /// Obtiene un listado paginado de Accounts
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro ?page lista los accounts, paginando 10 items por página. Debe tener el rol admin para efectuar esta operación.
        /// </remarks>
        /// <param name="page">Int, página solicitada.Debe ser mayor a 0.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve listado de accounts.</response>        
        /// <response code="404">Not Found. No se ha encontrado el objeto solicitado.</response> 
        /// <response code="500">Surgió un error inesperado.</response> 

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PagedList<AccountDto>>> GetAccounts([FromQuery] int page)
        {
            try
            {
                var result = await _accountServices.getAll(page);
                var response = new PagedList<AccountDto>(_mapper.Map<List<AccountDto>>(result.ToList()), result.TotalCount, result.CurrentPage, 10);
                if (response.Count > 0)
                    return Ok(response);
               
                return BadRequest(new { Status = "404", Message = "Error: Not found" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Status = "400", Message = $"Error: {ex.Message}" });
            }

        }

        // GET: Account/id
        /// <summary>
        /// Obtiene un Account específico a partir de su Id
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro id suministrado, obtiene el account correspondiente. Debe tener el rol admin para efectuar esta operación.
        /// </remarks>
        /// <param name="id">Int, id de cuenta solicitada.Debe ser mayor a 0.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado</response>        
        /// <response code="404">Not Found. No se ha encontrado el objeto solicitado, no existe Account con ese id</response> 
        /// <response code="500">Surgió un error inesperado.</response>

        [HttpGet("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await _accountServices.getById(id);

            if (account == null)
            {
                return NotFound(
                    new
                    {
                        Status = "Not found",
                        Message = "No account matches the id"
                    });
            }
            return Ok(account);
        }


        // PUT: Account/id
        /// <summary>
        /// Actualiza un Account a partir del model DTO AccountUpdateDto
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro modelo, actualiza un Account. El rol del usuario debe ser "Admin". Mediante la parametro (int) id
        /// se obtendrá el Account correspondiente y las propiedades del DTO se actualizarán las mismas en el Account. Se podrá modificar el CreationDate,
        /// el Money, el IsBlocked y el UserId. Si no se setea algún parámetro se dejará el existente.
        /// </remarks>
        /// <param name="id">Int, id del Account que se quiere actualizar. 
        /// <param name="accountDto">AccountUpdateDto, tiene como propiedades DateTime CreationDate, decimal Money, bool IsBlocked y int UserId. </param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve Ok y un mensaje de la correcta actualización del Account.</response>        
        /// <response code="400">Bad Request. La petición no cumple con las expectativas de parámetros</response> 
        /// <response code="500">Surgió un error inesperado.</response> 

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountUpdateDto accountDto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest("Check the data provided");

                var account = await _accountServices.getById(id);

                if (account == null)
                    return BadRequest("No account matches the id");

                await _accountServices.update(_mapper.Map(accountDto, account));
                //await _accountServices.update(_mapper.Map<AccountsEntity>(accountDto, account));
                //await _accountServices.update(id, accountDto);
                return Ok();
            }
            catch (Exception err)
            {
                return StatusCode(500, new { Status = "Server Error", Message = err.Message });
            }
        }

        // POST: Account/id
        /// <summary>
        /// Realizar una transferencia a una cuenta
        /// </summary>
        /// <remarks>
        /// Mediante el parametro TransferToAccountsDTO model, realiza una trasferencia de la cuenta con con el id que se pasa por parametro. 
        /// Se descontara del Amount del Account con el id pasado por parametro que debe pertenecer al usuario loguado y se sumara
        /// al de la cuenta receptora, utilizando los parámetros del DTO: 
        /// decimal Amount, int ToAccountId, string Concept, string Types. Adicionalmente se logua la tranferencia en Transactions y
        /// y se suma el 3% del Amount en puntos al usuario loguado. Solo un usuario con Rol: "Regular" podrá hacerlo.
        /// </remarks>
        /// <param name="model">int, id de la cuanta desde donde se quiere transferir (debe pertenecer al usuario loagueado).</param>
        /// <param name="model">AccountDto, tiene como propiedades decimal Amount, int ToAccountId, string Concept, string Types.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve Ok y un mensaje de la correcta transferencia.</response>        
        /// <response code="400">Bad Request. La petición no cumple con las expectativas de parámetros</response> 
        /// <response code="500">Surgió un error inesperado.</response>

        [HttpPost("{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> TransferToAccounts([FromBody] TransferToAccountsDTO model, int id)
        {
            try
            {
                if (!ModelState.IsValid || User.Identity?.Name == null)
                    return StatusCode(400, new { Status = "Bad Request", Message = "Some of the information in the transfer request between Accounts is invalid" }); 
                await _accountServices.TransferAccounts(model, id, User.Identity.Name);
                return Ok($"Successful transfer of ${model.Amount} successfully performed from Account:'{id}' to the Account:'{model.ToAccountId}'.");
            }
            catch (Exception err)
            {
                return StatusCode(500, new { Status = "Server Error", Message = $"Unable to transfer ${model.Amount} to the Account:'{model.ToAccountId}'. Error: {err.Message}" });
            }
        }

        // POST: Account
        /// <summary>
        /// Crear un Account a partir del model DTO AccountDto
        /// </summary>
        /// <remarks>
        /// Mediante el parametro modelo, crea un nuevo Account. Utilizando los parámetros del DTO:
        /// DateTime CreationDate, decimal Money, bool IsBlocked, int UserId, int Id. Solo un usuario con Rol: "Regular" podrá hacerlo.
        /// </remarks>
        /// <param name="model">AccountDto, tiene como propiedades DateTime CreationDate, decimal Money, bool IsBlocked, int UserId, int Id.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve Ok y un mensaje de la correcta creación del Account.</response>        
        /// <response code="400">Bad Request. La petición no cumple con las expectativas de parámetros</response> 
        /// <response code="500">Surgió un error inesperado.</response>

        [HttpPost]
        [Authorize(Roles = "Regular")]

        public async Task <IActionResult> CreateAccount([FromBody] AccountDto model)
        {
            AccountsEntity mappedModel = _mapper.Map<AccountsEntity>(model);
            try
            {
                await _accountServices.insert(mappedModel);
                return Ok();
            } 
            catch(Exception err)
            {
                return BadRequest($"Couldn't create account'. Error: {err.Message}");    
            }
        }

        // DELETE: Account/id
        /// <summary>
        /// Elimina un Account a partir de su id
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro indicado , elmina el account indicado.Debe tener el rol admin para efectuar esta operación.
        /// </remarks>
        /// <param name="id">Id del Account a eliminar.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve Ok y un mensaje de la correcta elminación del Account.</response>        
        /// <response code="404">Not Found. No se encontró un Account con el id indicado.</response> 
        /// <response code="500">Surgió un error inesperado.</response>

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                var account = await _accountServices.getById(id);

                if (account is null) 
                    return NotFound("No account matches the id");

                await _accountServices.DeleteAccount(account);

                return Ok("Account deleted");
            }
            catch (Exception err)
            {
                return StatusCode(500, new { Status = "Server Error", Message = err.Message });
            }
        }
    }
}
