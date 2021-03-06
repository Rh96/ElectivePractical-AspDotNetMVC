﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEDC.Lamazon.Homework.Services.Interfaces;
using SEDC.Lamazon.Homework.WebModels.ViewModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.Lamazon.Homework.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize(Roles = "user")]
        public IActionResult Products()
        {
            Log.Information("Fetching all products started");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            List<ProductViewModel> products = _productService.GetAllProducts().ToList();
            //if (products.Count > 0)
            //{
            //    return View("ErrorView");
            //}

            stopwatch.Stop();
            Log.Information($"Time for fetching all products: {stopwatch.ElapsedMilliseconds}ms");
            return View(products);
        }
    }
}
