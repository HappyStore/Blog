using Blog.Services.Role;
using Blog.Services.RoleUserManager;
using Blog.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Services.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogServices(this IServiceCollection services)
        {
            services.AddTransient<IBlogRoleManager, BlogRoleManager>();
            services.AddTransient<IBlogUserManager, BlogUserManager>();
            
            return services;
        }
    }
}