using Microsoft.AspNetCore.Mvc;
using SEDC.Lamazon.Homework.Services.Interfaces;
using SEDC.Lamazon.Homework.WebModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.Lamazon.Homework.Web.Controllers
{
    public class UserController : Controller
    {
        protected readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult LogIn()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult LogIn(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userService.Login(model);
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("listallorders", "order");
                    }
                    else
                    {
                        return RedirectToAction("products", "product");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult Register()
        {

            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _userService.Register(model);
                return RedirectToAction("products", "product");
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            _userService.LogOut();
            return RedirectToAction("index", "home");
        }
    }
}
