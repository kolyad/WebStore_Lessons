using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
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

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Create() => View("Edit", new EmployeeViewModel());

        #region Edit
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }

            var employee = _employeesData.Get(Id);
            if (employee is null)
            {
                return NotFound();
            }

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.Patronymic,
                BirthDate = employee.BirthDate,
                City = employee.City,
                HireDate = employee.HireDate
            });
        }


        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(EmployeeViewModel model)
        {
            _ = model ?? throw new ArgumentNullException();

            var employee = new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.MiddleName,
                BirthDate = model.BirthDate,
                City = model.City,
                HireDate = model.HireDate
            };

            if (employee.Id == 0)
            {
                _employeesData.Add(employee);
            }
            else
            {
                _employeesData.Update(employee);
            }

            return RedirectToAction("Index");
        }
        #endregion


        #region Delete
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }

            var employee = _employeesData.Get(Id);
            if (employee is null)
            {
                return NotFound();
            }

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.Patronymic,
                BirthDate = employee.BirthDate,
                City = employee.City,
                HireDate = employee.HireDate
            });
        }


        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult DeleteConfirmed(int Id)
        {
            _employeesData.Delete(Id);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
