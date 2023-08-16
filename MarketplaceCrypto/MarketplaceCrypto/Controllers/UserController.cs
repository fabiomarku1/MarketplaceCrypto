using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;

namespace MarketplaceCrypto.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IServiceManager _serviceManger;

        public UserController(IServiceManager serviceManger)
        {
            _serviceManger = serviceManger;
        }

        public async Task<IActionResult> Profile()
        {
            var userId=int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _serviceManger.UserService.GetUserById(userId);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateUserDTO request)
        {
            var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _serviceManger.UserService.UpdateUser(userId, request);
   
            return RedirectToAction("Profile", "User");

        }

    }
}
