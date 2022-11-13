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
        public async Task<ActionResult<List<UserDTO>>> GetUsers([FromQuery]int page)
        {
            var response = _mapper.Map<List<UserDTO>>( await _userService.getAll(page));

            if (response.Count == 0)
            {
                return NotFound();
            }

            return Ok(response);

        }


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

        [HttpPatch("product/{idProduct}")]
        [Authorize]
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