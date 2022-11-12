using AlkemyWallet.Repositories;
using AlkemyWallet.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlkemyWallet.Core.Services;
using AlkemyWallet.Entities;
using Microsoft.AspNetCore.Authorization;
using AlkemyWallet.DataAccess;
using AutoMapper;
using AlkemyWallet.Core.Models.DTO;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.Core.Helper;
using NSwag.Annotations;

namespace AlkemyWallet.Controllers
{

    [OpenApiTag("FixedTermDeposit",
            Description = "Web API para mantenimiento de Fixed Term Deposit.",
            DocumentationDescription = "Documentación externa",
            DocumentationUrl = "")]


    [Route("[controller]")]
    [ApiController]
    public class FixedTermDepositController : ControllerBase
    {
        private readonly IFixedTermDepositService _fixedTermDepositServices;
        private readonly IMapper _mapper;
        public FixedTermDepositController(IFixedTermDepositService FixedTermDepositServices,IMapper mapper)
        {
            _fixedTermDepositServices = FixedTermDepositServices;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets a specific Fixed Term Deposit.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [Authorize]
        [HttpGet("{id}")]
            public  async Task<IActionResult> GetFixedTermDepositById(int id)
        {
            FixedTermDepositEntity fixedDeposit = await _fixedTermDepositServices.getById(id);
            if (fixedDeposit == null) return NotFound(new { Status = "Not Fund", Message = "No FixedDeposit Fund" });
            else
            {

                var fixedDepositDto = _mapper.Map<FixedTermDepositDTO>(_fixedTermDepositServices.GetFixedTransactionDetailById(fixedDeposit));
                if (fixedDepositDto is null) return BadRequest(new { Status = "Not Fund", Message = "Not Fixed Deposit Found" });
                else return Ok(fixedDepositDto);



            }
        }

        // GET: api/FixedTermDeposit?page
        /// <summary>
        /// Obtiene un listado paginado de FixedTermDeposit
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro ?page lista los fixedTermDeposit, paginando 10 items por página. Solo traerá los ítems correspondientes al usuario logueado y para usuarios con rol "Regular".
        /// </remarks>
        /// <param name="page">Int, página solicitada.Debe ser mayor a 0.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado (Listado de Items Fixed, junto a dos string cuyas URL indican la anterior pagina y la posterior página).</response>        
        /// <response code="404">Not Found. No se ha encontrado el objeto solicitado, no existen FixedTermDeposit a nombre del usuario.</response> 
        /// <response code="500">Surgió un error inesperado.</response> 

        [HttpGet]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> GetAll([FromQuery] int page)
        {
            //Para cumplir con la firma del Helper
            var pagesParameters = new PagesParameters();
            pagesParameters.PageNumber = page;
            pagesParameters.PageSize = 10;
           
            
            try
            {
                string id = User.Identity.Name.ToString();
                PagedList<FixedTermDepositEntity> PagedList = await _fixedTermDepositServices.getAllbyUser(pagesParameters, id);

                if (PagedList != null)
                {

                    String NextUrl = "";
                    String PreviousUrl = "";
                    String ActionPath = Request.Host + Request.Path;

                    if (PagedList.HasNext)
                    {
                        NextUrl = "Next Page: " + ActionPath + "/page=" + (page + 1).ToString();
                    }
                    if (PagedList.HasPrevious)
                    {
                        PreviousUrl = "Previous Page: " + ActionPath + "/page=" + (page - 1).ToString();
                    }

                    var ListFixedDeposit = from p in PagedList select new FixedTermDepositItemDTO
                    {
                        Id = p.Id,
                        AccountId = (int)p.AccountId,
                        CreationDate = p.CreationDate,
                        ClosingDate = p.ClosingDate,
                        Amount = p.Amount
                    };


                    return Ok(new
                    {

                        NextURl = NextUrl,
                        PreviousURl = PreviousUrl,
                        ListFixedDeposit

                    });

                }
                else { return NotFound(new { Status = "Not Found", Message = "No FixedTermDeposits found." }); }
            
            }catch (Exception err)
            {
                return StatusCode(500, new { Status = "Server Error", Message = err.Message });
            }


        }

        // POST: api/FixedTermDeposit
        /// <summary>
        /// Crear un Fixed Term Deposit a partir del model DTO CreateFixedTermDepositDTO
        /// </summary>
        /// <remarks>
        /// Mediante el parametro Fixed Term Deposit DTO como modelo, crea un nuevo Fixed Term Deposit. Utilizando el username del usuario logueado, mas los parámetros del DTO:
        /// int AccountId, decimal Amount y datetime ClosingDate. El account id es el id de la cuenta de la cual se van a retirar los fondos para hacer el Fixed Term Deposit,
        /// esta cuenta debe ser del usuario logueado. El amount es el importe del fixed term deposit, debe ser mayor a 0, este se deducirá del saldo disponible de la cuenta. El closing date,
        /// es la fecha hora en la cual se terminará el Fixed Term Deposit y se retornarán los fondos a la cuenta con una tasa, debe ser mayor a 1 día. Solo un usuario con Rol: "Regular" podrá hacerlo.
        /// </remarks>
        /// <param name="model">Model DTO, tiene como propiedades AccountId, amount y Closing Date. Las validaciones se indican arriba.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve Ok y un mensaje de la correcta creación del Fixed Term Deposit con sus caracterrísticas.</response>        
        /// <response code="400">Bad Request. La petición no cumple con las expectativas de parámetros</response> 
        /// <response code="500">Surgió un error inesperado.</response> 

        [HttpPost]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> CreateFixedTermDeposit([FromBody] CreateFixedTermDepositDTO model) {

            if (ModelState.IsValid)
            {

                try
                {
                    string id = User.Identity.Name.ToString();
                    await _fixedTermDepositServices.CreateFixedTermDeposit(model, id);
                    return Ok($"Fixed Term Deposit created succesfully. Amount deposited: " + model.Amount + " . Closing Date: " + model.ClosingDate + ".");
            } catch (Exception Ex)
            {

                    return StatusCode(500, new { Status = "Server Error", Message = Ex.Message });

                }
        }
            else { return BadRequest();}

        }

        // PUT: api/FixedTermDeposit
        /// <summary>
        /// Actualiza un Fixed Term Deposit a partir de un modelo UpdateFixedTermDepositDTO
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro UpdateFixedTermDepositDTO como modelo, actualiza un Fixed Term Deposit. El rol del usuario debe ser "Admin". Mediante la propiedad (int) id del DTO
        /// se obtendrá el Fixed Term Deposit correspondiente y utilizando las otras propiedades del DTO se actualizarán las mismas en el Fixed Term Deposit. Se podrá modificar el Amount,
        /// el CreationDate y el Closing Date. Tener presente, dejar todos los parámetros con el valor correcto porque los actualizará indefectiblemente. Al ser un rol con perfil admin,
        /// no tiene las restricciones que posee un usuario con rol regular, por lo tanto, chequear bien los parámetros a actualizar considerando las restricciones existentes en el endpoint de Create.
        /// </remarks>
        /// <param name="model">Model DTO, tiene como propiedades Int Id (id del Fixed Term), decimal amount (monto del Fixed Term), creation (fecha de Creacion) y Closing Date. 
        /// Las validaciones se indican arriba.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve Ok y un mensaje de la correcta actualización del Fixed Term Deposit.</response>        
        /// <response code="400">Bad Request. La petición no cumple con las expectativas de parámetros</response> 
        /// <response code="500">Surgió un error inesperado.</response> 


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateFixedTermDeposit([FromBody] UpdateFixedTermDepositDTO model)
        {

            try 
            { 
                if (ModelState.IsValid) 
                {
                    await _fixedTermDepositServices.update(model);
                    return Ok("Fixed Term Deposit Updated");
                } 
                else 
                { 
                    return BadRequest("Check the data provided");
                }
            }
            catch(Exception ex) 
            {
                return StatusCode(500, new { Status = "Server Error", Message = ex.Message });

            }

        }

        // Delete: api/FixedTermDeposit
        /// <summary>
        /// Elmina un Fixed Term Deposit a partir de un id
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro indicado , elmina el fixed term deposit indicado.Debe tener el rol admin para efectuar esta operación.
        /// </remarks>
        /// <param name="id">Id del Fixed Term Deposit a eliminar.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve Ok y un mensaje de la correcta elminación del Fixed Term Deposit.</response>        
        /// <response code="404">Not Found. No se encontró un Fixed Term Deposit con el id indicado.</response> 
        /// <response code="500">Surgió un error inesperado.</response> 



        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFixedTermDeposit(int id)
        {
            try
            {
                FixedTermDepositEntity entity = await _fixedTermDepositServices.getById(id);
                if(entity != null)
                {
                    await _fixedTermDepositServices.delete(entity);
                    return Ok("Fixed Term Deposit Deleted");
                }
                else
                {
                    return NotFound("Fixed Term Deposit not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Server Error", Message = ex.Message });

            }

        }





    }
}
