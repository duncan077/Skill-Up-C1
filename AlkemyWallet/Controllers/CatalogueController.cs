using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AlkemyWallet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogueController : ControllerBase
    {

        private ICatalogueService _catalogueService;
        private IMapper _mapper;

        public CatalogueController(ICatalogueService catalogueService, IMapper mapper)
        {
            _mapper = mapper;
            _catalogueService = catalogueService;
        }

        [HttpGet]
        [Authorize(Roles = "Regular")]
        public async Task<ActionResult<List<CatalogueDTO>>> GetAll()

        {
            var response = await _catalogueService.getAllSortByPoints();
            if (response.Count == 0)
                return NotFound();
            return Ok(response);


        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> GetById(int id)
        {
            var catalogue = await _catalogueService.getById(id);

            if (catalogue == null)
            {
                return NotFound(
                    new
                    {
                        Status = "Not found",
                        Message = "No product catalogue matches the id"
                    });
            }
            return Ok(catalogue);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetProductsByUserPoints()
        {
            try
            {
                var claims = User.Claims.ToList();
                var user = await _catalogueService.getUserByUserName(claims[0].Value);
                if (user.Points.Equals(0))
                    return Ok($"You don't have points");
                var catalogue = await _catalogueService.GetCatalogueByUserPoints(user.Points);
                if (catalogue.Count.Equals(0))
                {
                    return Ok($"There are no products to redeem with your amount of points");
                }
                else
                {
                    return Ok(catalogue);
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

        }

    }
}
