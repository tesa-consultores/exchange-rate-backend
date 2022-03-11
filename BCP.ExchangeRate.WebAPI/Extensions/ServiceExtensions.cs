
using BCP.ExchangeRate.BusinessLogic;
using BCP.ExchangeRate.BusinessLogic.Interface;
using BCP.ExchangeRate.BusinessLogic.Interfaces;
using BCP.ExchangeRate.Repository;
using BCP.ExchangeRate.Repository.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace BCP.ExchangeRate.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {            
            services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
        }

        public static void ConfigureServicesManager(this IServiceCollection services)
        {
            services.AddScoped<IUserBL, UserBL>();
            services.AddScoped<IExchangeRateBL, ExchangeRateBL>();
        }


    }
}
