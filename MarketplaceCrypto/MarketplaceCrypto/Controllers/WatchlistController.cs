using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace MarketplaceCrypto.Controllers
{
    public class WatchlistController : Controller
    {
        private readonly IServiceManager _serviceManager;


        public WatchlistController(IServiceManager serviceManager, HttpClient httpClient)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> List()
        {
            var userId=int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var list = await _serviceManager.WatchlistService.GetWatchlistItems(userId);
            if(list is not null)
             return View(list);
           
            return View();
        }

        public async Task<IActionResult> Remove(string symbol)
        {
            var userId=int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _serviceManager.WatchlistService.RemoveFromWatchlist(userId, symbol);
            return RedirectToAction("List", "Watchlist");
        }
    }
}
