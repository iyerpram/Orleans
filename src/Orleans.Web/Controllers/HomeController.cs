using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orleans.Web.Grains;
using Orleans.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Orleans.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClusterClient ClusterClient;

        public HomeController(ILogger<HomeController> logger, IClusterClient clusterClient)
        {
            _logger = logger;
            ClusterClient = clusterClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Order(Order order)
        {
            if (order == null || !order.Products.Any())
                return BadRequest("Invalid Order");

            await ClusterClient.GetGrain<CustomerGrain>(Guid.NewGuid()).OrderProduct(order);
            return Ok("Order Created");
        }        
    }
}
