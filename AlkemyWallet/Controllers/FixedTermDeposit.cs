using AlkemyWallet.Repositories;
using AlkemyWallet.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlkemyWallet.Core.Services;
using AlkemyWallet.Entities;
using Microsoft.AspNetCore.Authorization;
using AlkemyWallet.DataAccess;
using AutoMapper;
using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixedTermDepositController : ControllerBase
    {
        private readonly IFixedTermDepositServices _fixedTermDepositServices;
        private readonly WalletDbContext _context;

        public FixedTermDepositController(IFixedTermDepositServices FixedTermDepositServices)
        {
            _fixedTermDepositServices = FixedTermDepositServices;
        }

        [Route("api/[Controller]")]
        [Authorize(Roles ="Regular")]
        [HttpGet("{id}")]
        public  async Task<IActionResult> GetFixedTermDepositById(int id)
        {
            FixedTermDepositEntity fixedDeposit = await _fixedTermDepositServices.getById(id);
            if (fixedDeposit == null) return NotFound(new { Status = "Not Fund", Message = "No FixedDeposit Fund" });
            else
            {
                var fixedDepositDto = _fixedTermDepositServices.GetFixedTransactionDetailById(fixedDeposit);
                if (fixedDepositDto is null) return BadRequest(new { Status = "Not Fund", Message = "Not Fixed Deposit Fund" });
                else return Ok(fixedDepositDto);

            }
        }
    }
}