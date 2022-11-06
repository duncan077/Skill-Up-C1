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
        private readonly IMapper _mapper;

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


    }
}
