﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using TraversalCoreProje.Areas.Admin.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
  
        [AllowAnonymous]
        [Area("Admin")]
        public class BookingHotelSearchController : Controller
        {
            public async Task<IActionResult> Index()
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://booking-com.p.rapidapi.com/v2/hotels/search?order_by=popularity&adults_number=2&checkin_date=2023-09-27&filter_by_currency=AED&dest_id=-553173&locale=en-gb&checkout_date=2023-09-28&units=metric&room_number=1&dest_type=city&include_adjacency=true&children_number=2&page_number=0&children_ages=5%2C0&categories_filter_ids=class%3A%3A2%2Cclass%3A%3A4%2Cfree_cancellation%3A%3A1"),
                    Headers =
    {
     { "X-RapidAPI-Key", "1051d7e15cmsh95600c481039dc2p1922dfjsnba37f02ef1c9" },
        { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var bodyReplace = body.Replace(".", "");
                    var values = JsonConvert.DeserializeObject<BookingHotelSearchViewModel>(bodyReplace);
                    return View(values.result);
                }
            }

            [HttpGet]
            public IActionResult GetCityDestID()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> GetCityDestID(string p)
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/locations?locale=en-gb&name={p}"),
                    Headers =
    {
        { "X-RapidAPI-Key", "cb5ee15da1mshb46d59d679af3abp1fe84cjsn167590fdc0cc" },
        { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    return View();
                }
            }
        }
}
