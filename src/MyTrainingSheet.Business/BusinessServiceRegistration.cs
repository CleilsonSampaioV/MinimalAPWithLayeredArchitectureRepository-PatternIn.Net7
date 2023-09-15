using Microsoft.Extensions.DependencyInjection;

namespace MyTrainingSheet.Business
{
    public static class BusinessServiceRegistration
    {
        public static IServiceCollection AddBusinnesServices(this IServiceCollection services)
        {
            services.AddScoped<ILiftService, LiftManager>();

            return services;
        }
    }
}
