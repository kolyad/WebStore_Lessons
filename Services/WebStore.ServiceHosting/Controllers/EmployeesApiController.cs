using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Domain.Models;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesApiController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }

        [HttpGet] // GET http://localhost:5001/api/employees
        public IEnumerable<Employee> Get()
        {
            return _employeesData.Get();
        }

        [HttpGet("{id}")] // GET http://localhost:5001/api/employees/5
        public Employee Get(int id)
        {
            return _employeesData.Get(id);
        }

        [HttpGet("employee")] // GET http://localhost:5001/api/employees/employee?lastName=xxxx&firstName=yyyyy&patronymic=zzzzz
        public Employee GetByName(string lastName, string firstName, string patronymic)
        {
            return _employeesData.GetByName(lastName, firstName, patronymic);
        }

        [HttpPost] // POST http://localhost:5001/api/employees
        public int Add(Employee employee)
        {
            return _employeesData.Add(employee);
        }

        [HttpPost("employee")] // POST http://localhost:5001/api/employees/employee/?lastName=xxxx&firstName=yyyyy&patronymic=zzzzz&age=20
        public Employee Add(string lastName, string firstName, string patronymic, int age)
        {
            return _employeesData.Add(lastName, firstName, patronymic, age);
        }

        [HttpPut] // PUT http://localhost:5001/api/employees
        public void Update(Employee employee)
        {
            _employeesData.Update(employee);
        }

        [HttpDelete] // DELETE http://localhost:5001/api/employees
        public bool Delete(int id)
        {
            return _employeesData.Delete(id);
        }
    }
}
