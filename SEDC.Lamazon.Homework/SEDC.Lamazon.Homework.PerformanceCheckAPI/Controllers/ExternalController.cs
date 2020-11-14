using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.Lamazon.Homework.Services.Interfaces;

namespace SEDC.Lamazon.Homework.PerformanceCheckAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public ExternalController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("performance/getorder")]
        public ActionResult<long> GetUserOrderPerformance()
        {
            string userId = "0dbdf80d-b8f7-49a9-aca6-7cc050ea42b5";
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 10; i++)
            {
                _orderService.GetCurrentOrder(userId);
            }

            stopwatch.Stop();
            long time = stopwatch.ElapsedMilliseconds;
            return time;
        }
    }
}
