using SEDC.Lamazon.Homework.Domain.Models;
using SEDC.Lamazon.Homework.WebModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Lamazon.Homework.Services.Interfaces
{
    public interface IUserService
    {
        //TODO: Change all the User domain models with appropriate ViewModel!!!
        void Register(RegisterViewModel registerModel);
        void Login(LoginViewModel loginModel);
        void LogOut();
        UserViewModel GetCurrentUser(string username);
    }
}
