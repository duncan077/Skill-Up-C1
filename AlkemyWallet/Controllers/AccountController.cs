using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace AlkemyWallet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {



        private IAccountService _accountServices;
        private IMapper _mapper;

        public AccountController(IAccountService accountServices, IMapper mapper)
        {
            _mapper = mapper;
            _accountServices = accountServices;
        }

        [HttpGet]

        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<AccountDto>>> GetAccounts([FromQuery] AccountDto param,[FromQuery]RolesDTO oaram)

        {
            var response = await _accountServices.ListAccounts();
           
            if (response.Count() == 0) return NotFound();
            return Ok(response);
                    }

        [HttpGet("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await _accountServices.getById(id);

            if (account == null)
            {
                return NotFound(
                    new
                    {
                        Status = "Not found",
                        Message = "No account matches the id"
                    });
            }
            return Ok(account);
        }

        /*[HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountUpdateDto accountDto)
        {
            try
            {
                var account = await _accountServices.getById(id);

                if (account is null) 
                    return NotFound("No account matches the id");

                await _accountServices.update(_mapper.Map(accountDto, account));
                //await _accountServices.update(_mapper.Map<AccountsEntity>(accountDto));
                return Ok();
            }
            catch (Exception err)
            {
                return StatusCode(500, new { Status = "Server Error", Message = err.Message });
            }
        }*/


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountUpdateDto accountDto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest("Check the data provided");

                var account = await _accountServices.getById(id);

                if (account == null)
                    return BadRequest("No account matches the id");

                await _accountServices.update(_mapper.Map(accountDto, account));
                //await _accountServices.update(_mapper.Map<AccountsEntity>(accountDto, account));
                //await _accountServices.update(id, accountDto);
                return Ok();
            }
            catch (Exception err)
            {
                return StatusCode(500, new { Status = "Server Error", Message = err.Message });
            }
        }


        [HttpPost("{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> TransferToAccounts([FromBody] TransferToAccountsDTO model, int id)
        {
            try
            {
                if (!ModelState.IsValid || User.Identity?.Name == null) return BadRequest("Some of the information in the transfer request between Accounts is invalid");
                await _accountServices.TransferAccounts(model, id, User.Identity.Name);
                return Ok($"Successful transfer of ${model.Amount} successfully performed from Account:'{id}' to the Account:'{model.ToAccountId}'.");
            }
            catch (Exception err)
            {
                return BadRequest($"Unable to transfer ${model.Amount} to the Account:'{model.ToAccountId}'. Error: {err.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Regular")]

        public async Task <IActionResult> CreateAccount([FromBody] AccountDto model)
        {
            AccountsEntity mappedModel = _mapper.Map<AccountsEntity>(model);
            try
            {
                await _accountServices.insert(mappedModel);
                return Ok();
            } 
            catch(Exception err)
            {
                return BadRequest($"Couldn't create account'. Error: {err.Message}");    
            }
        }
    }
}
