using AlkemyWallet.Repositories;
using AlkemyWallet.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlkemyWallet.Entities;
using Microsoft.AspNetCore.Authorization;
using AlkemyWallet.Core.Models.DTO;
using AutoMapper;
using System.Collections.Generic;
using AlkemyWallet.Core.Services.ResourceParameters;

namespace AlkemyWallet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesServices;
        private readonly  IMapper _mapper;

        public RolesController(IRolesService rolesServices, IMapper mapper)

        {
            _rolesServices = rolesServices;
            _mapper = mapper;

        }

        /// <summary>
        /// Devuelve una lista de Roles
        /// </summary>
        /// <param name="rolesParams">Lista de parametros para paginar</param>      
        /// <response code="200">OK. Listado con Exito</response>    
        /// <response code="400">Bad Request. No se ha podido validar la transaccion.</response>
        /// <response code="403 ">Unauthorized request. No esta autorizado para usar este recurso</response>
        /// <response code="500">Server Error. Erores del Servidor</response>  
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetRoles([FromQuery]PagesParameters rolesParams)
        {
            try
            {
                var listRoles = _mapper.Map<IReadOnlyList<RolesDTO>>(await _rolesServices.getAll(rolesParams));
                if (listRoles is null)
                    return NotFound(new { Status = "Not Found", Message = "No Role Fount" });

                return Ok(listRoles);
            }
            catch (Exception err)
            {
                return StatusCode(500, new { Status = "Server Error", Message = err.Message });
            }
            
        }



        /// <summary>
        /// Muestra el detallo de un Rol
        /// </summary>
        /// <param name="id">"Id" Numero identificador del Rol</param>
        /// <response code="200">OK. Creado con exito.</response>        
        /// <response code="400">Bad Request. No se ha podido validar la transaccion.</response>   
        /// <response code="403 ">Unauthorized request. No esta autorizado para usar este recurso</response>
        /// <response code="500">Server Error. Erores del Servidor</response>  
        [Authorize]
        [HttpGet("{id}")]
        public async Task <IActionResult> GetRoleDetail(int id)
        { 
            RolesDTO rol = _mapper.Map<RolesDTO>(await _rolesServices.getById(id));
            if (rol is null) return BadRequest(new { Status = "Not Role Fund", Message = "" });
            else return Ok(rol);
        }

        /// <summary>
        /// Agrega un nuevo rol
        /// </summary>
        /// <remarks>
        /// Ejemplo de Campos requeridos_
        /// {
        ///     "Name" : "string",
        ///     "Description": "string"
        /// }
        /// </remarks>
        /// <response code="200">OK. Creado con exito.</response>        
        /// <response code="400">Bad Request. No se ha podido validar la transaccion.</response>   
        /// <response code="403 ">Unauthorized request. No esta autorizado para usar este recurso</response>
        /// <response code="500">Server Error. Erores del Servidor</response>  
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddRole([FromBody] RolesDTO role)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("");
                    return Ok(await _rolesServices.insert(role));
            }
            catch (Exception err)
            {
                return StatusCode(500, new { Status = "Server Error", Message = err.Message });
            }
           

        }


        /// <summary>
        /// Elimina un Rol
        /// </summary>
        /// <param name="id">"Id" Identificador del Rol a eliminar </param>
        /// <response code="200">OK. Creado con exito.</response>        
        /// <response code="400">Bad Request. No se ha podido validar la transaccion.</response>   
        /// <response code="403 ">Unauthorized request. No esta autorizado para usar este recurso</response>
        /// <response code="500">Server Error. Erores del Servidor</response> 
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var rol = await _rolesServices.getById(id);
                if (rol is null) return NotFound("We can't find a Role with the submitted Id");
                await _rolesServices.delete(rol);
                return Ok("Successfully deleted Role"); 
            }
            catch (Exception err)
            {
                return StatusCode(500, new { Status = "Server Error", Message = err.Message });
            }
        }


        /// <summary>
        /// Modifica un Rol con nueva información
        /// </summary>
        /// <remarks>
        /// Ejemplo de Campos requeridos_
        /// {
        ///     "Name" : "string",
        ///     "Description": "string"
        /// }
        /// </remarks>
        /// <param name="id">Id identificador del Rol a modificar</param>
        /// <response code="200">OK. Creado con exito.</response>        
        /// <response code="404">Not Foundt. No se ha podido validar la transaccion.</response>   
        /// <response code="500">Server Error. No se ha indicado o es incorrecto el Token JWT de acceso.</response>  
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole([FromBody] RolesDTO roleDto, int id)
        {
            try
            {
                var role = await _rolesServices.getById(id);
                if (role is null) return NotFound("We can't find a Role with the submitted Id");
                return Ok(await _rolesServices.update(_mapper.Map(roleDto, role))); 
            }
            catch (Exception err)
            {
                return StatusCode(500, new { Status = "Server Error", Message = err.Message });
            }
        }

    }


    
}
