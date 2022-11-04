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




    }
}
