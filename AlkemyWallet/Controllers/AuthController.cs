using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AlkemyWallet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JWTAuthManager _authManager;
        private readonly IUserService _userService;

        public AuthController( JWTAuthManager authManager, IUserService userService)
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
                if (user is null|| user.IsDeleted)
                {
                    return Unauthorized();
                }
                if (_authManager.VerifyPasswordHash(loginDTO.password, Encoding.Default.GetBytes(user.Password)))
                {
                    var token = _authManager.CreateToken(user.Email, user.Role.Name);
                    return Accepted(token);
                }
                return BadRequest();
            }
            catch (Exception)
            {

                return BadRequest();
            }
            

           
        }
    }
}
