using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WebStore.Clients.Base;
using WebStore.Domain.Models;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Clients
{
    public class EmloyeesClient : BaseClient, IEmployeesData
    {
        public EmloyeesClient(IConfiguration configuration) : base(configuration, WebApi.Employees) { }

        public IEnumerable<Employee> Get()
        {
            throw new System.NotImplementedException();
        }

        public Employee Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Employee GetByName(string lastName, string firstName, string patronymic)
        {
            throw new System.NotImplementedException();
        }

        public int Add(Employee employee)
        {
            throw new System.NotImplementedException();
        }

        public Employee Add(string lastName, string firstName, string patronymic, int age)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Employee employee)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
