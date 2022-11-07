using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Services;
using AlkemyWallet.Core.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq.Expressions;

namespace AlkemyWallet.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

    
        [HttpGet("{id}")]
        //[Authorize(Roles = "Regular")]
        public async Task<IActionResult> GetById(int id)
        {
           

            var user = await userService.getById(id);

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
        
        


    }
}