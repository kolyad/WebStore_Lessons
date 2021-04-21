using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using WebStore.Clients.Base;
using WebStore.Domain.Models;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Clients
{
    public class EmloyeesClient : BaseClient, IEmployeesData
    {
        public EmloyeesClient(IConfiguration configuration) : base(configuration, WebApi.Employees) { }

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(Address);

        public Employee Get(int id) => Get<Employee>($"{Address}/{id}");

        public Employee GetByName(string lastName, string firstName, string patronymic) =>
            Get<Employee>($"{Address}/employee?lastName={lastName}&firstName={firstName}&patronymic={patronymic}");

        public int Add(Employee employee) => 
            Post(Address, employee)
            .Content
            .ReadAsAsync<int>()
            .Result;

        public Employee Add(string lastName, string firstName, string patronymic, int age) => 
            Post($"{Address}/employee?lastName={lastName}&firstName={firstName}&patronymic={patronymic}", "")
            .Content
            .ReadAsAsync<Employee>()
            .Result;


        public void Update(Employee employee) => Put(Address, employee);

        public bool Delete(int id) => Delete($"{Address}/{id}").IsSuccessStatusCode;
    }
}
