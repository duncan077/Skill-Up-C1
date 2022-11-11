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
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.Core.Helper;

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
            if (fixedDeposit == null) return NotFound(new { Status = "Not Fund", Message = "No FixedDeposit Fund" });
            else
            {

                var fixedDepositDto = _mapper.Map<FixedTermDepositDTO>(_fixedTermDepositServices.GetFixedTransactionDetailById(fixedDeposit));
                if (fixedDepositDto is null) return BadRequest(new { Status = "Not Fund", Message = "Not Fixed Deposit Fund" });
                else return Ok(fixedDepositDto);



            }
        }

        [Authorize(Roles = "Regular")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page)
        {
            //Para cumplir con la firma del Helper
            var pagesParameters = new PagesParameters();
            pagesParameters.PageNumber = page;
            pagesParameters.PageSize = 10;

            try
            {

                PagedList<FixedTermDepositEntity> PagedList = _fixedTermDepositServices.getAll(pagesParameters).Result;

                if (PagedList != null)
                {

                    String NextUrl = "";
                    String PreviousUrl = "";
                    String ActionPath = Request.Host + Request.Path;

                    if (PagedList.HasNext)
                    {
                        NextUrl = "Next Page: " + ActionPath + "/page=" + (page + 1).ToString();
                    }
                    if (PagedList.HasPrevious)
                    {
                        PreviousUrl = "Previous Page: " + ActionPath + "/page=" + (page - 1).ToString();
                    }

                    return Ok(new
                    {

                        NextURl = NextUrl,
                        PreviousURl = PreviousUrl,
                        PagedList

                    });

                }
                else { return NotFound(new { Status = "Not Found", Message = "No FixedTernDeposits found." }); }
            
            }catch (Exception err)
            {
                return StatusCode(500, new { Status = "Server Error", Message = err.Message });
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
                    return Ok($"Fixed Term Deposit created succesfully. Amount deposited: " + model.Amount + " . Closing Date: " + model.ClosingDate + ".");
            } catch (Exception Ex)
            {

                return BadRequest(Ex.Message);

            }
        }
            else { return BadRequest();}

        }
  
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateFixedTermDeposit([FromBody] UpdateFixedTermDepositDTO model)
        {

            try 
            { 
                if (ModelState.IsValid) 
                {
                    await _fixedTermDepositServices.update(model);
                    return Ok("Fixed Term Deposit Updated");
                } 
                else 
                { 
                    return BadRequest("Check the data provided");
                }
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFixedTermDeposit(int id)
        {
            try
            {
                FixedTermDepositEntity entity = await _fixedTermDepositServices.getById(id);
                if(entity != null)
                {
                    await _fixedTermDepositServices.delete(entity);
                    return Ok("Fixed Term Deposit Deleted");
                }
                else
                {
                    return BadRequest("Check the data provided");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }





    }
}
