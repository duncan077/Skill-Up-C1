using AlkemyWallet.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public RolesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


    }
}
