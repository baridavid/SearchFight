using Application.Interfaces;
using Infrastructure.External;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ISearchClientAsync, GoogleSearchClient>();
            services.AddTransient<ISearchClientAsync, BingSearchClient>();

            return services;
        }
    }
}
