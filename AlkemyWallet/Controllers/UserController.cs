using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Services;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq.Expressions;
using AutoMapper;
using System.Security.Claims;

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

        [HttpPatch("block/{id}")]
        [Authorize]
        public async Task<IActionResult> BlockAccountById(int id)
        {
            try {
                var claims = User.Claims.ToList();
                var user = await _userService.getByUserName(claims[0].Value);
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

    }
}