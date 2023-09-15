using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTrainingSheet.Infra.Context;

namespace MyTrainingSheet.Infra
{
    public static class InfraServiceRegistration
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            var con = configuration.GetConnectionString("TrainingSheetConnectionString");
            services.AddDbContext<MyTranningSheetContext>(options =>
                options.UseNpgsql(con));

            services.AddScoped<ILiftRepository, LiftRepository>();

            return services;
        }
    }
}
