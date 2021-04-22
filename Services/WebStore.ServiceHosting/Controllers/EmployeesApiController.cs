using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<EmployeesApiController> _logger;

        public EmployeesApiController(
            IEmployeesData employeesData,
            ILogger<EmployeesApiController> logger)
        {
            _employeesData = employeesData;
            _logger = logger;
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
            _logger.LogInformation("Добавление нового сотрудника {0}", employee);
            
            return _employeesData.Add(employee);
        }

        [HttpPost("employee")] // POST http://localhost:5001/api/employees/employee/?lastName=xxxx&firstName=yyyyy&patronymic=zzzzz&age=20
        public Employee Add(string lastName, string firstName, string patronymic, int age)
        {
            _logger.LogInformation("Добавление нового сотрудника {0} {1} {2} {3} лет", 
                lastName, firstName, patronymic, age);

            return _employeesData.Add(lastName, firstName, patronymic, age);
        }

        [HttpPut] // PUT http://localhost:5001/api/employees
        public void Update(Employee employee)
        {
            _logger.LogInformation("Редактирование сотрудника {0}", employee);

            _employeesData.Update(employee);
        }

        [HttpDelete("{id}")] // DELETE http://localhost:5001/api/employees/5
        public bool Delete(int id)
        {
            _logger.LogInformation("Удаление сотрудника с Id {0} ...", id);

            var result = _employeesData.Delete(id);

            _logger.LogInformation("Удаление сотрудника с Id {0} - {1}", id, 
                result ? "выполнено" : "не найден");

            return _employeesData.Delete(id);
        }
    }
}
