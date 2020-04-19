using Microsoft.Extensions.DependencyInjection;

namespace Blog.Services.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogServices(this IServiceCollection services)
        {
            return services;
        }
    }
}