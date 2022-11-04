using AlkemyWallet.Repositories;
using AlkemyWallet.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlkemyWallet.Core.Services;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixedTermDepositController : ControllerBase
    {
        private readonly IFixedTermDepositServices _fixedTermDepositServices;

        public FixedTermDepositController(IFixedTermDepositServices FixedTermDepositServices)
        {
            _fixedTermDepositServices = FixedTermDepositServices;
        }


    }
}