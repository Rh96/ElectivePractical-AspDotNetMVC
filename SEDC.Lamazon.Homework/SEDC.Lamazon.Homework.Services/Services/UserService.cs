﻿using Microsoft.AspNetCore.Identity;
using SEDC.Lamazon.Homework.DataAccess.Interfaces;
using SEDC.Lamazon.Homework.Domain.Enums;
using SEDC.Lamazon.Homework.Domain.Models;
using SEDC.Lamazon.Homework.Services.Interfaces;
using SEDC.Lamazon.Homework.WebModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Lamazon.Homework.Services.Services
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly SignInManager<User> _signInManager;
        protected readonly UserManager<User> _userManager;
        public UserService(IUserRepository userRepository, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public void Register(RegisterViewModel registerModel)
        {
            User user = new User
            {
                UserName = registerModel.Username,
                FullName = string.Format("{0} {1}", registerModel.Firstname, registerModel.Lastname),
                Email = registerModel.Email,
                Orders = new List<Order>
                {
                    new Order
                    {
                        Status = StatusType.Init
                    }
                }
            };

            var password = registerModel.Password;

            var result = _userManager.CreateAsync(user, password).Result;

            if (result.Succeeded)
            {
                var currentUser = _userManager.FindByNameAsync(user.UserName).Result;
                _userManager.AddToRoleAsync(user, "user");

                Login(new LoginViewModel
                {
                    Username = registerModel.Username,
                    Password = registerModel.Password
                });
            }
        }

        public void Login(LoginViewModel loginModel)
        {
            var result = _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, false).Result;

            if (result.IsNotAllowed)
            {
                throw new Exception("Username or Password is not correct!");
            }
        }

        public UserViewModel GetCurrentUser(string username)
        {

            try
            {
                User user = _userRepository.GetByUsername(username);
                return new UserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Fullname = user.FullName
                };
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw new Exception(message);
            }
        }

        public void LogOut()
        {
            _signInManager.SignOutAsync();
        }
    }
}