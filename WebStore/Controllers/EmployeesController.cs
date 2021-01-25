using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private List<Employee> _employees;

        public EmployeesController()
        {
            _employees = TestData.Employees;
        }

        public IActionResult Index() => View(_employees);

        public IActionResult Details(int Id)
        {
            var model = _employees.FirstOrDefault(x => x.Id == Id);

            if (model is not null)
                return View(model);

            return NotFound();
        }
    }
}
