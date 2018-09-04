using BackSide2.BL.authorize;
using BackSide2.DAO.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BackSide2.Extentions
{
    public static class ConfigureContainerExtensions
    {
        public static void AddRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void AddScopedServices(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddTransient<IPersonService, PersonService>();
            serviceCollection.AddScoped<ITokenService, TokenService>();
        }
    }
}
