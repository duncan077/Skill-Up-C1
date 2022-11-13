using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Services;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq.Expressions;
using AutoMapper;
using System;
using System.Text;
using NSwag.Annotations;

namespace AlkemyWallet.Controllers
{
    [OpenApiTag("User",
        Description = "Web API para mantenimiento de Users.",
        DocumentationDescription = "Documentación externa",
        DocumentationUrl = "")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IJWTAuthManager _jWTAuthManager;

        public UserController(IUserService userService, IMapper mapper, IJWTAuthManager jWTAuthManager)
        {
            _userService = userService;
            _mapper = mapper;
            _jWTAuthManager = jWTAuthManager;
        }

        // GET: /User
        /// <summary>
        /// Obtiene una lista de usuarios
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro id suministrado, obtiene la lista de usuarios en el sistema con opción de paginación. Se debe estar autenticado con rol de administrador.
        /// </remarks>
        /// <param name="page">Int, página solicitada.Debe ser mayor a 0.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado (Listado de Usuarios, junto a dos string cuyas URL indican la anterior pagina y la posterior página).</response>        
        /// <response code="404">Not Found. No se han encontrado usuarios.</response> 
        /// <response code="500">Surgió un error inesperado.</response> 
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            var response = await _userService.getAll();

            if (response.Count == 0)
                return NotFound();

            return Ok(response);

        }

        // GET: /User/1
        /// <summary>
        /// Obtiene un usuario por su Id.
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro id suministrado, obtiene un usuario. Se debe estar autenticado con rol de "Regular".
        /// </remarks>
        /// <param name="id">Int, Id del usuario solicitado.Debe ser mayor a 0.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado (Un usuario).</response>        
        /// <response code="404">Not Found. No se ha encontrado el objeto solicitado, no existen usuario con ese id.</response> 
        /// <response code="500">Surgió un error inesperado.</response> 
        [HttpGet("{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> GetById(int id)
        {


            var user = await _userService.getById(id);

            if (user == null)
            {
                return NotFound(
                    new
                    {
                        Status = "Not found",
                        Message = "No user matches the id"
                    });
            }



            return Ok(user);
        }

        // PUT: /User/1
        /// <summary>
        /// Actualiza un usuario
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro id suministrado, actualiza usuario en el sisteman. Se debe estar autenticado con rol "Regular".
        /// </remarks>
        /// <param name="id">Int, usuario a actualizar.Debe ser mayor a 0.</param>
        /// <param name="firstName">String, nombre del usuario. Debe ser enviado por body</param>
        /// <param name="lastName">String, apellido del usuario. Debe ser enviado por body</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. El usuario ha sido actualizado correctamente.</response>        
        /// <response code="404">Not Found. No se ha encontrado el usuario solicitado.</response> 
        /// <response code="500">Surgió un error inesperado.</response> 
        [HttpPut("{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userData)
        {
            var user = await _userService.getById(id);

            if (user == null)
            {
                return NotFound(
                    new
                    {
                        Status = "Not found",
                        Message = "No user matches the id"
                    });
            }

            try
            {
                await _userService.update(_mapper.Map<UserEntity>(userData));
                return Ok();
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    Status = "Server Error",
                    Message = err.Message
                });
            }
        }

        // PATCH: /User/1
        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro id suministrado, elimina usuario en el sistema (Soft delete). Se debe estar autenticado con rol "Admin".
        /// </remarks>
        /// <param name="id">Int, usuario a eliminar.Debe ser mayor a 0.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. El usuario ha sido eliminado correctamente.</response>        
        /// <response code="404">Not Found. No se ha encontrado el usuario solicitado.</response> 
        /// <response code="500">Surgió un error inesperado.</response> 
        [Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                UserEntity response = await _userService.getById(id);
                if(response is null)
                {
                    return NotFound("User not found");
                }
                response.IsDeleted = true;
                await _userService.update(response);
                await _userService.saveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST: /User/1
        /// <summary>
        /// Crea un usuario
        /// </summary>
        /// <remarks>
        /// Crea un usuario en el sistema. Se debe estar autenticado con rol "Regular".
        /// </remarks>
        /// <param name="firstName">String, nombre del usuario. Debe ser enviado por body</param>
        /// <param name="lastName">String, apellido del usuario. Debe ser enviado por body</param>
        /// <param name="email">String, email del usuario. Debe ser enviado por body</param>
        /// <param name="password">String, contraseña del usuario. Debe ser enviado por body</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. El usuario ha sido creado correctamente.</response>        
        /// <response code="500">Surgió un error inesperado.</response> 
        [HttpPost]
        [Authorize(Roles = "Regular")]
        public async Task<ActionResult> CreateUser(CreateUserDTO request)
        {
            if (request is not null)
            {
                UserEntity user = _mapper.Map<UserEntity>(request);
                byte[] password = _jWTAuthManager.CreatePasswordHash(request.Password);
                user.Password = Convert.ToBase64String(password);
                user.RoleId = 2;
                try
                {
                    await _userService.insert(user);
                    await _userService.saveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Can�t create the user, please check the data");
            }
            return Ok("User created successfully");
        }

        [HttpPatch("product/{idProduct}")]
        [Authorize]
        public async Task<IActionResult> RedeemProduct(int idProduct)
        {
            try {
                var userName = User.Identity.Name.ToString();
                var user = await _userService.getByUserName(userName);
                if (user.Points.Equals(0))
                    return BadRequest($"You don't have points to redeem");
                var product = await _userService.GetCatalogueById(idProduct);
                if (product == null)
                    return BadRequest($"Doesn't exist the product {idProduct}");
                if (user.Points < product.Points)
                    return BadRequest($"You don't have points to redeem this product, Points: {user.Points}.");
                user.Points -= product.Points;
                await _userService.update(user);
                await _userService.saveChanges();
                return Ok($"You have successfully redeemed the product ** {product.ProductDescription} **. Points deducted: {product.Points}. New points balance: {user.Points}.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}