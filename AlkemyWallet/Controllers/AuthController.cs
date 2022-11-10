using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AlkemyWallet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTAuthManager _authManager;
        private readonly IUserService _userService;

        public AuthController(IJWTAuthManager authManager, IUserService userService)
        {
            _authManager = authManager;
            _userService = userService;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginPost(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var user = await _userService.getByUserName(loginDTO.userName);
                if (user is null)
                {
                    return Unauthorized();
                }
                if (!_authManager.VerifyPasswordHash(loginDTO.password, Encoding.Default.GetBytes(user.Password)))
                {
                    var token = _authManager.CreateToken(user.Email, user.Role.Name);
                    return Ok(token);
                }
                return BadRequest();
            }
            catch (Exception)
            {

                return BadRequest();
            }


        }
        [HttpGet("me")]
        [Authorize]

        public async Task<IActionResult> AuthenticateUser()
        {
            try
            {
                var userName = User.Identity?.Name?.ToString();
                var userRole = User.Claims.Select(claim => claim.Type == "Role").First().ToString();
                return Ok(new
                {
                    UserName = userName,
                    Role = userRole
                });
            }
            catch (Exception err)
            {
                return BadRequest(new
                {
                    Status = "Bad request",
                    Message = err.Message
                });
            }
        }
    }
}
