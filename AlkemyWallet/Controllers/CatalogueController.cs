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
    }
}
