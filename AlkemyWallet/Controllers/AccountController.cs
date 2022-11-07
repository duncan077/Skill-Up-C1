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
        public async Task<ActionResult<List<AccountDto>>> GetAccounts()

        {
            var response =await _accountServices.ListedAccounts();
            if (response.Count==0)
                return NotFound();
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

        [HttpPost("accounts/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TransferToAccounts([FromBody] TransferToAccountsDTO model, int id)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Some of the information in the transfer request between Accounts is invalid");
                await _accountServices.TransferAccounts(model, id, User.Identity.Name);
                return Ok($"Successful transfer of ${model.Amount} successfully performed from Account:'{id}' to the Account:'{model.ToAccountId}'.");
            }
            catch (Exception err)
            {
                return BadRequest($"Unable to transfer ${model.Amount} to the Account:'{model.ToAccountId}'. Error: {err.Message}");
            }
        }

    }
}
