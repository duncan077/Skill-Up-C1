using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories;
using AlkemyWallet.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyWallet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles="Admin")]
    public class AccountController : ControllerBase
    {



        private IAccountService _accountServices;

        public AccountController(IAccountService accountServices)
        {
            _accountServices = accountServices;
        }

        [HttpGet]

        public IActionResult GetAccounts()
        {

            return Ok();


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await _accountServices.GetAccountById(id);

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



    }
}
