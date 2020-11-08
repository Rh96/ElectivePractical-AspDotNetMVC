using SEDC.Lamazon.Homework.Domain.Enums;
using SEDC.Lamazon.Homework.Domain.Models;
using SEDC.Lamazon.Homework.WebModels.Enums;
using SEDC.Lamazon.Homework.WebModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Lamazon.Homework.Services.Interfaces
{
    public interface IOrderService
    {
        // TODO: Change all Order domain models with appropriate ViewModel
        IEnumerable<OrderViewModel> GetAllOrders();
        OrderViewModel GetOrderById(int id, string userId);
        OrderViewModel GetOrderById(int id);
        int CreateOrder(OrderViewModel order, string userId);
        int AddProduct(int orderId, int productId, string userId);
        OrderViewModel GetCurrentOrder(string userId);
        int ChangeStatus(int orderId, string userId, StatusTypeViewModel status);
    }
}
