using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        private readonly IUserOperationClaimService _userOperationClaimService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }

        [HttpPost]
        public IActionResult Add(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimService.Add(userOperationClaim);
            return Ok("kullanıcı yetkilendirme işlemi başarıyla tamamlandı");
        }
    }
}
