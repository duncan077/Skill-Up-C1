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

namespace AlkemyWallet.Controllers
{
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
                return BadRequest("Can´t create the user, please check the data");
            }
            return Ok("User created successfully");
        }
    }
}