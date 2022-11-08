using AlkemyWallet.Repositories;
using AlkemyWallet.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlkemyWallet.Core.Services;
using AlkemyWallet.Entities;
using Microsoft.AspNetCore.Authorization;
using AlkemyWallet.Core.Models.DTO;
using AutoMapper;
using System.Collections.Generic;


namespace AlkemyWallet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesServices;
        private readonly  IMapper _mapper;

        public RolesController(IRolesService rolesServices, IMapper mapper)

        {
            _rolesServices = rolesServices;
            _mapper = mapper;

        }


        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetRoles()
        {
            var listRoles = _mapper.Map<IReadOnlyList<RolesDTO>>(await _rolesServices.getAll());

            if (listRoles is null)
                return NotFound( new { Status = "Not Found", Message = "No Role Fount"});

            return Ok(listRoles);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task <IActionResult> GetRoleDetail(int id)
        { 
            RolesDTO rol = _mapper.Map<RolesDTO>(await _rolesServices.getById(id));
            if (rol is null) return BadRequest(new { Status = "Not Role Fund", Message = "" });
            else return Ok(rol);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddRole([FromBody] RolesDTO role)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("");
                    return Ok(await _rolesServices.insert(role));
            }
            catch (Exception err)
            {
                return StatusCode(500, new { Status = "Server Error", Message = err.Message });
            }
           

        }
    }


    
}
