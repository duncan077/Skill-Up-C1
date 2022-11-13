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
using AlkemyWallet.Repositories.Interfaces;

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

        // GET: api/FixedTermDeposit/id
        /// <summary>
        /// Obtiene un Fixed Term Deposit específico a partir de su Id
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro id suministrado, obtiene el fixed term deposit correspondiente al usuario logueado y para usuarios con rol  "Regular".
        /// </remarks>
        /// <param name="id">Int, Id del Fixed Term Deposit a consultar  .</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado (Listado de Items Fixed, junto a dos string cuyas URL indican la anterior pagina y la posterior página).</response>        
        /// <response code="404">Not Found. No se ha encontrado el objeto solicitado, no existen FixedTermDeposit a nombre del usuario.</response> 
        /// <response code="500">Surgió un error inesperado.</response> 

        [Authorize(Roles = "Regular")]
        [HttpGet("{id}")]
            public  async Task<IActionResult> GetFixedTermDepositById(int id)
        {
          
            string username = User.Identity.Name.ToString();
            FixedTermDepositEntity fixedDeposit = await _fixedTermDepositServices.GetFixedTermDepositDetail(id,username);

            if (fixedDeposit == null) return StatusCode(404, new { Status = "No Fixed Terms Deposit Found for the logged user", Message = "No FixedTermDeposits found for the logged user that matches with the ID provided." });
            else
            {
                    FixedTermDepositDTO fixedDepositDTO = new FixedTermDepositDTO();
                fixedDepositDTO.Id = id;
                    fixedDepositDTO.CreationDate = fixedDeposit.CreationDate;
                    fixedDepositDTO.UserId = (int)fixedDeposit.UserId;
                    fixedDepositDTO.AccountId = (int)fixedDeposit.AccountId;
                    fixedDepositDTO.Amount = fixedDeposit.Amount;
                    fixedDepositDTO.ClosingDate = fixedDeposit.ClosingDate;
                    return Ok(fixedDepositDTO);
               
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

                    string NextUrl = string.Empty;
                    string PreviousUrl = string.Empty;
                    string ActionPath = Request.Host + Request.Path;
                    NextUrl = PagedList.HasNext ? $"Next Page: {ActionPath} ?page= {(page + 1)}" : string.Empty;
                    PreviousUrl = PagedList.HasPrevious ? $"Previous Page: {ActionPath} ?page= {(page - 1)}" : string.Empty;

                    var ListFixedDeposit = from p in PagedList select new FixedTermDepositDTO
                    {
                        Id = p.Id,
                        AccountId = (int)p.AccountId,
                        CreationDate = p.CreationDate,
                        ClosingDate = p.ClosingDate,
                        Amount = p.Amount,
                        UserId = (int)p.UserId
                    };




                    return Ok(new
                    {

                        NextURl = NextUrl,
                        PreviousURl = PreviousUrl,
                        ListFixedDeposit

                    });

                }
                else { return StatusCode(404, new { Status = "No Fixed Terms Deposit Found", Message = "No FixedTermDeposits found." }); }
            
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
            else { return StatusCode(400, new { Status = "Bad Request", Message = "Check the request parameters" }); }

        }

        // PUT: api/FixedTermDeposit
        /// <summary>
        /// Actualiza un Fixed Term Deposit a partir del model DTO UpdateFixedTermDepositDTO
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro UpdateFixedTermDepositDTO como modelo, actualiza un Fixed Term Deposit. El rol del usuario debe ser "Admin". Mediante la propiedad (int) id del DTO
        /// se obtendrá el Fixed Term Deposit correspondiente y utilizando las otras propiedades del DTO se actualizarán las mismas en el Fixed Term Deposit. Se podrá modificar el Amount,
        /// el CreationDate y el Closing Date. Si no se setea algún parámetro se dejará el existente.
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

                    FixedTermDepositEntity fixedTermDepositEntity = await _fixedTermDepositServices.getById((int)model.id);
                    if (fixedTermDepositEntity != null) 
                    {

                        if (model.Amount is not null)  fixedTermDepositEntity.Amount = (decimal)model.Amount;
                        if (model.ClosingDate is not null)  fixedTermDepositEntity.ClosingDate = (DateTime)model.ClosingDate; 
                        if (model.CreationDate is not null) fixedTermDepositEntity.CreationDate = (DateTime)model.CreationDate; 

                        await _fixedTermDepositServices.update(fixedTermDepositEntity);

                        return Ok("Fixed Term Deposit Updated");
                    }
                    else{return StatusCode(404, new { Status = "Not Found", Message = "No Fixed Term Deposit matches with the Id provided" }); }
                } 
                else 
                {
                    return StatusCode(400, new { Status = "Bad Request", Message = "Check the request parameters" });
                }
            }
            catch(Exception ex) 
            {
                return StatusCode(500, new { Status = "Server Error", Message = ex.Message });

            }

        }

        // Delete: api/FixedTermDeposit
        /// <summary>
        /// Elimina un Fixed Term Deposit a partir de su id
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
                    return StatusCode(404, new { Status = "Not Found", Message = "No Fixed Term Deposit matches with the Id provided" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Server Error", Message = ex.Message });

            }

        }





    }
}
