﻿using System.Security.Claims;
using System.Text.Json;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.Contracts;
using Shared.Utility;

namespace MarketplaceCrypto.Controllers
{
    [Authorize]
    public class CryptoController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly HttpClient _httpClient;

        public CryptoController(IServiceManager serviceManager, HttpClient httpClient)
        {
            _serviceManager = serviceManager;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> ListCoins()
        {
            var list = await _serviceManager.BinanceService.GetAllCryptos(null);
            return View(list); 
        }

        [HttpGet("ListCoins/Sort")]
        public async Task<IActionResult> Sort(string? sortBy)
        {
            var list = await _serviceManager.BinanceService.GetAllCryptos(sortBy);
            return View(list);
        }

        public async Task<IActionResult> DetailsBySymbol(string symbol)
        {
            var userId=int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
           //     HttpContext.Session.SetString("CryptoSymbol", symbol);
            await _serviceManager.WatchlistService.AddItemToWatchlist(symbol, userId);

            var dataList = await _serviceManager.BinanceService.GetDataForSymbol(symbol,null);
            if (dataList is null)
            {
                throw new NotFoundException("No data list was found for symbol");
                return View();

            }
        
            return View("DetailsBySymbol",dataList.ToList());

        }


        public async Task<IActionResult> Interval(string symbol,string interval, DateTime ?startDate, DateTime? endDate)
        {
           if(interval.Equals("custom"))
            {
                 var data = await _serviceManager.BinanceService.GetDataForCustomRange(symbol,startDate.Value,endDate.Value);
                   return View("DetailsBySymbol",data.ToList());
            }


            var dataList = await _serviceManager.BinanceService.GetDataForSymbol(symbol,interval);
            if (dataList is null)
            {
                throw new NotFoundException("No data list was found for symbol");
                return View();

            }

              return View("DetailsBySymbol",dataList.ToList());

        }

        //[HttpGet("ListCoins")]
        //public async Task<IActionResult> ListCoins(string? pattern)
        //{
        //    if (string.IsNullOrEmpty(pattern))
        //    {
        //        var list = await _serviceManager.BinanceService.GetAllCryptos();
        //        return View(list); 
        //    }
        //    else
        //    {
        //        var list = await _serviceManager.CryptocurrencyService.GetCryptocurrencyByPattern(pattern);
        //        return View(list);
        //    }
        //}

        public async Task<IActionResult> Pattern(string pattern)
        {
            var list = await _serviceManager.CryptocurrencyService.GetCryptocurrencyByPattern(pattern);
            return View(list);
        }


    }
    
}
