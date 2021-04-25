using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Clients.Identity;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Clients.Clients.Identity
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddApiClientsIdentityWebStores(this IServiceCollection services)
        {
            services.AddTransient<IUserStore<User>, UsersClient>();
            services.AddTransient<IUserRoleStore<User>, UsersClient>();
            services.AddTransient<IUserPasswordStore<User>, UsersClient>();
            services.AddTransient<IUserEmailStore<User>, UsersClient>();
            services.AddTransient<IUserPhoneNumberStore<User>, UsersClient>();
            services.AddTransient<IUserTwoFactorStore<User>, UsersClient>();
            services.AddTransient<IUserClaimStore<User>, UsersClient>();
            services.AddTransient<IUserLoginStore<User>, UsersClient>();

            services.AddTransient<IRoleStore<Role>, RolesClient>();

            return services;
        }

        public static IdentityBuilder AddApiClientsIdentityWebStores(this IdentityBuilder builder)
        {
            builder.Services.AddApiClientsIdentityWebStores();
            return builder;
        }
    }
}
