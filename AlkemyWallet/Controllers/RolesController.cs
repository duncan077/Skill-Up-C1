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
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesServices _rolesServices;
        private readonly  IMapper _mapper;

        public RolesController(IRolesServices rolesServices, IMapper mapper)
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

        [Route("api/[Controller]")]
        [Authorize]
        [HttpGet("{id}")]
        public async Task <IActionResult> GetRoleDetail(int id)
        { 
            RolesDTO rol = _mapper.Map<RolesDTO>(await _rolesServices.getById(id));
            if (rol is null) return BadRequest(new { Status = "Not Role Fund", Message = "" });
            else return Ok(rol);
        }
    }


    
}
