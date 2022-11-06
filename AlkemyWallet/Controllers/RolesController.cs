using AlkemyWallet.Repositories;
using AlkemyWallet.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlkemyWallet.Core.Services;
using AlkemyWallet.Entities;
using Microsoft.AspNetCore.Authorization;

namespace AlkemyWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesServices _rolesServices;

        public RolesController(IRolesServices rolesServices)
        {
            _rolesServices = rolesServices;
        }


        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetRoles()
        {
            var listRoles = await _rolesServices.getAll();

            if (listRoles is null)
                return NotFound( new { Status = "Not Found", Message = "No Role Fount"});

            return Ok(listRoles);
        }


    }
}
