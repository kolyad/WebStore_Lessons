using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<EmloyeesClient> _logger;

        public EmloyeesClient(
            IConfiguration configuration,
            ILogger<EmloyeesClient> logger) : base(configuration, WebApi.Employees)
        {
            _logger = logger;
        }

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(Address);

        public Employee Get(int id) => Get<Employee>($"{Address}/{id}");

        public Employee GetByName(string lastName, string firstName, string patronymic) =>
            Get<Employee>($"{Address}/employee?lastName={lastName}&firstName={firstName}&patronymic={patronymic}");

        public int Add(Employee employee)
        {
            _logger.LogInformation("Добавление нового сотрудника {0}", employee);

            return Post(Address, employee)
                .Content
                .ReadAsAsync<int>()
                .Result;
        }

        public Employee Add(string lastName, string firstName, string patronymic, int age)
        {
            _logger.LogInformation("Добавление нового сотрудника {0} {1} {2} {3} лет",
               lastName, firstName, patronymic, age);

            return Post($"{Address}/employee?lastName={lastName}&firstName={firstName}&patronymic={patronymic}", "")
                .Content
                .ReadAsAsync<Employee>()
                .Result;
        }


        public void Update(Employee employee)
        {
            _logger.LogInformation("Редактирование сотрудника {0}", employee);

            Put(Address, employee);
        }


        public bool Delete(int id)
        {
            _logger.LogInformation("Удаление сотрудника с Id {0} ...", id);

            var result = Delete($"{Address}/{id}").IsSuccessStatusCode;

            _logger.LogInformation("Удаление сотрудника с Id {0} - {1}", id,
                result ? "выполнено" : "не найден");

            return result;
        }
    }
}
