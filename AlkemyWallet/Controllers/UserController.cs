using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AlkemyWallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            var response = await _userService.getAll();

            if (response.Count == 0)
                return NotFound();

            return Ok(response);

        }
    }
}