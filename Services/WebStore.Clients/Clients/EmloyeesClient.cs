using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Interfaces;

namespace WebStore.Clients.Clients
{
    public class EmloyeesClient : BaseClient
    {
        public EmloyeesClient(IConfiguration configuration) : base(configuration, WebApi.Employees) { }
    }
}
