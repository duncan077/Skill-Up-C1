using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var response = _mapper.Map<List<AccountDto>>(await _accountServices.getAll());
            if(response.Count==0)
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



    }
}
