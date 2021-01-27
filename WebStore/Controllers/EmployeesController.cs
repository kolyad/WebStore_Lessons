using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }

        public IActionResult Index() => View(_employeesData.Get());

        public IActionResult Details(int Id)
        {
            var model = _employeesData.Get(Id);

            if (model is not null)
                return View(model);

            return NotFound();
        }
    }
}
