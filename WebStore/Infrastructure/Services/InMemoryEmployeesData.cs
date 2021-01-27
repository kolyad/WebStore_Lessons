using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<Employee> _employees;
        private int _currenctMaxId;
        public InMemoryEmployeesData()
        {
            _employees = TestData.Employees;
        }

        public IEnumerable<Employee> Get() => _employees;

        public Employee Get(int id) => _employees.FirstOrDefault(e => e.Id == id);
        
        public int Add(Employee employee)
        {
            _ = employee ?? throw new ArgumentNullException();            
            
            if (_employees.Contains(employee))
            {
                return employee.Id;
            }

            employee.Id = ++_currenctMaxId;
            _employees.Add(employee);
            return employee.Id;
        }

        public void Update(Employee employee)
        {
            _ = employee ?? throw new ArgumentNullException();

            if (_employees.Contains(employee))
            {
                return;
            }

            var db_item = Get(employee.Id);
            if (db_item is null)
            {
                return;
            }

            db_item.FirstName = employee.FirstName;
            db_item.LastName = employee.LastName;
            db_item.Patronymic = employee.Patronymic;
            db_item.BirthDate = employee.BirthDate;
            db_item.City = employee.City;
            db_item.HireDate = employee.HireDate;
        }

        public bool Delete(int id)
        {
            var db_item = Get(id);
            if (db_item is null)
            {
                return false;
            }
            return _employees.Remove(db_item);
        }
    }
}
