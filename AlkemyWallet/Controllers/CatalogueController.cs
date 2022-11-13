using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services;
using AlkemyWallet.Entities;
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
                return StatusCode(404, new { Status = "No Item Catalogue found", Message = "No Item Catalogue found." });
            }
            return Ok(catalogue);
        }


        private bool ValidateStirng(string cadena, int min, int max)
        {
            return (!string.IsNullOrEmpty(cadena)) && cadena.Length >= min && cadena.Length <=max;
        }

        private bool validateCatalogue(CatalogueEntity catalogue)
        {
            return (catalogue.Points != null && catalogue.Points >= 0) && (ValidateStirng(catalogue.Image,4,50) && (ValidateStirng(catalogue.ProductDescription,4,200)));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostCatalogue([FromBody] CatalogueEntity catalogue)
        {
            if (validateCatalogue(catalogue)/*!(catalogue is null) ||ModelState.IsValid*/)
            {
                await _catalogueService.insert(_mapper.Map<CatalogueEntity>(catalogue));
                return Ok(new { message = "Add Success", Code=200});
            }
            else return BadRequest(new { message = "Error" , Code=500});
        }


        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCatalogueDetail(int id, string detail)
        {
            CatalogueEntity catalogueUpdate = await _catalogueService.getById(id);
            if (!(catalogueUpdate is null))
            {
                catalogueUpdate.ProductDescription = string.IsNullOrEmpty(detail) ? catalogueUpdate.ProductDescription : detail;
                await _catalogueService.update(catalogueUpdate);
                return Ok(new { message= "Updae catalogue success", Code=200});
            }
            else return BadRequest(new { message = "Error"});

        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCatalogByid(int id)
        {
            if (id <= 0) return BadRequest("Id must be positive");
            else
            {
                try
                {
                    await _catalogueService.delete(await _catalogueService.getById(id));
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
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
