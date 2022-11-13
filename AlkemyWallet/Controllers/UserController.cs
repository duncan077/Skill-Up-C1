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

using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;
using AlkemyWallet.Core.Helper;

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
        public async Task<ActionResult<List<UserDTO>>> GetUsers([FromQuery]int page)
        {
            var response = _mapper.Map<List<UserDTO>>( await _userService.getAll(page));

            if (response.Count == 0)
            {
                return NotFound();
            }

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

            try 
            { 
                var user = await _userService.getById(id);

                if (user == null)
                {
                    return StatusCode(404, new { Status = "No user found", Message = "No user matches the Id" });
                }

            var UserDetailDTO = new UserDetailDTO();
            UserDetailDTO.FirstName = user.FirstName;
            UserDetailDTO.LastName = user.LastName;
            UserDetailDTO.Email = user.Email;
            UserDetailDTO.Accounts = user.Accounts.Select(x => x.Id.ToString()).ToList();
            return Ok(UserDetailDTO);
            
            }catch (Exception err)
            {
                return StatusCode(500, new { Status = "Server Error", Message = err.Message});
            }
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



        // PATCH: User/block/Id
        /// <summary>
        /// Bloquea un usuario a partir de su ID.
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro id suministrado, obtiene y bloquea el usuario solicitado.Lo puede realizar el rol Regular. /// </remarks>
        /// <param name="id">Int, Id del usuario a bloquear.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. El usuario ha sido bloqueado.</response>        
        /// <response code="404">Not Found. No se ha encontrado el objeto solicitado, no existen Usuario con ese ID.</response> 
        /// <response code="500">Surgió un error inesperado.</response> 

        [HttpPatch("block/{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> BlockAccountById(int id)
        {
            try {
                var userName = User.Identity.Name.ToString();
                var user = await _userService.getByUserName(userName);
                var account = await _userService.GetAccountByID(id);
                if (account is null)
                    return NotFound($"The account doesn't exist");
                if (!user.Id.Equals(account.UserId))
                    return Unauthorized($"You're not authorize to block this account");
                if (account.IsBlocked)
                    return BadRequest($"The account is already blocked");
                await _userService.blockAccount(account);
                return Ok($"The account has been blocked successfully");
            }
            catch(Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

        }


        // DELETE: User/id
        /// <summary>
        /// Borra un usuario a partir de su Id.
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro id suministrado, borra el usuario correspondiente. Solo puede ser realizado por el rol "Admin".
        /// </remarks>
        /// <param name="id">Int, Id del Fixed Term Deposit a consultar  .</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado (Listado de Items Fixed, junto a dos string cuyas URL indican la anterior pagina y la posterior página).</response>        
        /// <response code="404">Not Found. No se ha encontrado el objeto solicitado, no existen Users con el id indicado.</response> 
        /// <response code="500">Surgió un error inesperado.</response> 


        [Authorize(Roles = "Admin")]
        [HttpDelete]
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
        [AllowAnonymous]
        public async Task<ActionResult> CreateUser(CreateUserDTO request)
        {
            if (request is not null)
            {
                UserEntity user = _mapper.Map<UserEntity>(request);
                 
                user.Password = _jWTAuthManager.CreatePasswordHash(request.Password);
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



        // PATCH: api/UserController/product/IdProduct
        /// <summary>
        /// Mediante el id de producto indicado, realiza un canje de productos.
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro id suministrado, obtiene y canjear el producto solicitado. Esto reduce el saldo de puntos del usuario.Lo puede realizar el rol Regular. 
        /// </remarks>
        /// <param name="id">Int, Id del Producto a canjear.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve mensaje que se realizo correctamente el canje.</response>        
        /// <response code="404">Not Found. No se ha encontrado el objeto solicitado, no existen Productos con el Id indicado.</response> 
        /// <response code="500">Surgió un error inesperado.</response> 


        [HttpPatch("product/{idProduct}")]
        [Authorize(Roles = "Regular")]
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



        // PATCH: api/UserController/id
        /// <summary>
        /// A partir del Id de una cuenta, la desbloquea.
        /// </summary>
        /// <remarks>
        /// Mediante el parámetro id suministrado, obtiene y desbloquea el usuario solicitado. Lo puede realizar el rol Regular.
        /// </remarks>
        /// <param name="id">Int, Id de la cuenta a desbloquear .</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Obtiene un mensaje indicando el desbloqueo de la cuenta.</response>        
        /// <response code="404">Not Found. No se ha encontrado el objeto solicitado, no existen cuentas con ese ID.</response> 
        /// <response code="500">Surgió un error inesperado.</response> 

        [HttpPatch("unblock/{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> UnblockAccountById(int id)
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var user = await _userService.getByUserName(userName);
                var account = await _userService.GetAccountByID(id);
                if (account is null)
                    return NotFound($"The account doesn't exist");
                if (!user.Id.Equals(account.UserId))
                    return Unauthorized($"You're not authorize to unblock this account");
                if (!account.IsBlocked)
                    return BadRequest($"The account is already unblocked");
                await _userService.unblockAccount(account);
                return Ok($"The account has been unblocked successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

        }

    }
}