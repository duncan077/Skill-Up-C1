using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AlkemyWallet.Controllers
{

    [OpenApiTag("AuthController",
           Description = "Web API para mantenimiento de Autenticación",
           DocumentationDescription = "Documentación externa",
           DocumentationUrl = "")]


    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTAuthManager _authManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IJWTAuthManager authManager, IUserService userService, IMapper mapper)
        {
            _authManager = authManager;
            _userService = userService;
            _mapper = mapper;  
        }



        // POST: api/Auth/login/
        /// <summary>
        ///Realiza un login a partir del usuario y clave proporcionado. Devuelve un api key para utilizar los endpoints.
        /// </summary>
        /// <remarks>
        /// Realiza un login a partir del usuario y clave proporcionado. Devuelve un api key para utilizar los endpoints según los roles .
        /// </remarks>
        /// <param name="loginDTO">Login DTO model, posee username del tipo string y password del tipo string.</param>
        /// <response code="200">OK. Devuelve el token JWT para autenticar las request.</response>        
        /// <response code="400">Bad request. User y password incorrectas.</response> 
        /// <response code="500">Surgió un error inesperado.</response> 

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginPost(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var user = await _userService.getByUserName(loginDTO.userName);
                if (user is null|| user.IsDeleted)
                {
                    return Unauthorized();
                }
                if (_authManager.VerifyPasswordHash(loginDTO.password, user.Password))
                {
                    var token = _authManager.CreateToken(user.Email, user.Role.Name);
                    return Ok(token);
                }
                return BadRequest(new { Status = "Bad Request", Message = "Error: Wrong User or password" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Status = "Bad Request", Message = $"Error: {ex.Message}" });
            }


        }


        // GET: api/Auth/
        /// <summary>
        /// Obtiene el detalle de usuario, a partir del usuario logueado.
        /// </summary>
        /// <remarks>
        /// Obtiene el dato de usuario, a partir del usuario logueado./// </remarks>
        /// <response code="200">OK. Devuelve el token JWT para autenticar las request.</response>        
        /// <response code="400">Bad request. No hay usuario logueado.</response> 
        /// <response code="500">Surgió un error inesperado.</response> 

        [HttpGet("me")]
        [Authorize]

        public async Task<IActionResult> GetUserData()
        {
            try
            {
                var userName = User.Identity?.Name?.ToString();
                if (userName == null) return BadRequest(new
                {
                    Status = "Bad Request",
                    Message = "username null"
                });
                var userData = await _userService.getByUserName(userName);
                var response = _mapper.Map<UserDTO>(userData);
                return Ok(response);
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    Status = "server error",
                    Message = err.Message
                });
            }
        }
    }
}
