using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Services;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq.Expressions;
using AutoMapper;

namespace AlkemyWallet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _userService.getById(id);
                if(response is null)
                {
                    return NotFound("User not found");
                }
                await _userService.delete(_mapper.Map<UserEntity>(response));
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}