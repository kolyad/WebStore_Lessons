using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Orders")]
        public async Task<IActionResult> OrdersAsync([FromServices] IOrderService orderService)
        {
            var orders = await orderService.GetUserOrdersAsync(User.Identity.Name);
            return View(orders.Select(order => new UserOrderViewModel
            {
                Id = order.Id,
                Name = order.Name,
                Phone = order.Phone,
                Address = order.Address,
                TotalPrice = order.Items.Sum(item => item.Price * item.Quantity),
            }));
        }
    }
}
