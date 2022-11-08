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
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AlkemyWallet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FixedTermDepositController : ControllerBase
    {
        private readonly IFixedTermDepositService _fixedTermDepositServices;
        private readonly IMapper _mapper;
        public FixedTermDepositController(IFixedTermDepositService FixedTermDepositServices,IMapper mapper)
        {
            _fixedTermDepositServices = FixedTermDepositServices;
            _mapper = mapper;
        }

      
        [Authorize]
        [HttpGet("{id}")]
        public  async Task<IActionResult> GetFixedTermDepositById(int id)
        {
            FixedTermDepositEntity fixedDeposit = await _fixedTermDepositServices.getById(id);
            if (fixedDeposit == null) return NotFound(new { Status = "Not Found", Message = "No FixedDeposit Found" });
            else
            {
                //if (_fixedTermDepositServices.GetUserById((int)fixedDeposit.UserId)!=null)
                //{
                //    return Ok(_mapper.Map<FixedTermDepositDTO>(fixedDeposit));
                //}
                return BadRequest(new { Status = "Not Fund",Message="Not Fixed Deposit Fund"});
            }
        }

   

   
        [HttpPost]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> CreateFixedTermDeposit([FromBody] CreateFixedTermDepositDTO model) {

            if (ModelState.IsValid)
            {

                try
                {
                    await _fixedTermDepositServices.CreateFixedTermDeposit(model);
                    return Ok($"Fixed Term Deposit created succesfully. Amount deposited: " + model.Amount + "Closing Date: " + model.ClosingDate + ".");
            } catch (Exception Ex)
            {

                return BadRequest(Ex.Message);

            }
        }
            else { return BadRequest();}

        }


    }
}