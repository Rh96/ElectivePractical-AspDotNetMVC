using Microsoft.AspNetCore.Mvc;
using SEDC.Lamazon.Homework.Services.Interfaces;
using SEDC.Lamazon.Homework.Web.Models;
using SEDC.Lamazon.Homework.WebModels.Enums;
using SEDC.Lamazon.Homework.WebModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.Lamazon.Homework.Web.Controllers
{
    public class OrderController : Controller
    {
        protected readonly IOrderService _orderService;
        protected readonly IProductService _productService;
        protected readonly IUserService _userService;

        public OrderController(IOrderService orderService, IProductService productService, IUserService userService)
        {
            _orderService = orderService;
            _productService = productService;
            _userService = userService;
        }
        
        public IActionResult ListOrders()
        {
            string userId = "3";
            List<OrderViewModel> userOrders = _orderService.GetAllOrders()
                                                .Where(x => x.User.Id == userId)
                                                .ToList();
            return View(userOrders);
        }

        public IActionResult ListAllOrders()
        {
            List<OrderViewModel> orders = _orderService.GetAllOrders().ToList();
            return View(orders);
        }

        public IActionResult OrderDetails(int orderId)
        {
            string userId = "3";
            OrderViewModel order = _orderService.GetOrderById(orderId, userId);

            if (order.Id > 0)
            {
                return View("order", order);
            }
            else
            {
                return View("Error", new ErrorViewModel());
            }
        }

        public IActionResult Order()
        {
            string userId = "3";
            OrderViewModel order = _orderService.GetCurrentOrder(userId);
            return View(order);
        }

        public IActionResult ApproveOrder(int orderId)
        {
            OrderViewModel order = _orderService.GetOrderById(orderId);
            _orderService.ChangeStatus(order.Id, order.User.Id, StatusTypeViewModel.Confirmed);
            return RedirectToAction("listallorders");
        }

        public IActionResult DeclineOrder(int orderId)
        {
            OrderViewModel order = _orderService.GetOrderById(orderId);
            _orderService.ChangeStatus(order.Id, order.User.Id, StatusTypeViewModel.Declined);
            return RedirectToAction("listallorders");
        }

        public int AddProduct(int productId)
        {
            string userId = "3";
            OrderViewModel order = _orderService.GetCurrentOrder(userId);

            int result = _orderService.AddProduct(order.Id, productId, userId);

            if (result >= 0)
            {
                return result;
            }
            else
            {
                string message = "Something bad happened! Please contact support!";
                return result;
            }
        }
    }
}
