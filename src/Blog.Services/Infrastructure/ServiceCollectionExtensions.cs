using Blog.Services.Role;
using Blog.Services.RoleUserManager;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Services.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogServices(this IServiceCollection services)
        {
            services.AddTransient<IBlogRoleManager, BlogRoleManager>();

            return services;
        }
    }
}