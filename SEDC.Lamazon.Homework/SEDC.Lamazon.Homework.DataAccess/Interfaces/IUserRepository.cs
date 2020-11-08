using SEDC.Lamazon.Homework.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Lamazon.Homework.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        User GetById(string id);
    }
}
