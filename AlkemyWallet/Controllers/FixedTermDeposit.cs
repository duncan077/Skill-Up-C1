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
    [Route("[controller]")]
    [ApiController]
    public class FixedTermDepositController : ControllerBase
    {
        private readonly IFixedTermDepositService _fixedTermDepositServices;
        private readonly WalletDbContext _context;
        private readonly IMapper _mapper;
        public FixedTermDepositController(IFixedTermDepositService FixedTermDepositServices, WalletDbContext context, IMapper mapper)
        {
            _fixedTermDepositServices = FixedTermDepositServices;
            _context = context;
            _mapper = mapper;
        }

      
        [Authorize]
        [HttpGet("{id}")]
        public  async Task<IActionResult> GetFixedTermDepositById(int id)
        {
            FixedTermDepositEntity fixedDeposit = await _fixedTermDepositServices.getById(id);
            if (fixedDeposit == null) return NotFound(new { Status = "Not Fund", Message = "No FixedDeposit Fund" });
            else
            {
                if (fixedDeposit.UserId == _context.Users.Find(fixedDeposit.UserId).Id)
                {
                    return Ok(_mapper.Map<FixedTermDepositDTO>(fixedDeposit));
                }
                return BadRequest(new { Status = "Not Fund",Message="Not Fixed Deposit Fund"});
            }
        }
    }
}