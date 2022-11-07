using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Services;
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
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok();
        }

    
        [HttpGet("{id}")]
        //[Authorize(Roles = "Regular")]
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
        
        


    }
}