using Blog.DataAccess.EntityModels.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.DataAccess.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBlogDatabase(
            this IServiceCollection services,
            string connectionString = null)
        {
            services
                .AddDbContext<BlogDbContext>((provider, builder) =>
                {
                    builder
                        .UseLazyLoadingProxies()
                        .UseNpgsql(connectionString ??
                                   provider.GetService<IConfiguration>()
                                       .GetConnectionString("BlogDb"));
                })
                .AddIdentity<User, Role>(options =>
                {
                    options.Password.RequiredLength = 4;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddUserStore<UserStore<
                    User, Role, BlogDbContext, int, IdentityUserClaim<int>, UserRole,
                    IdentityUserLogin<int>, IdentityUserToken<int>, IdentityRoleClaim<int>>>()
                .AddRoleStore<RoleStore<Role, BlogDbContext, int, UserRole, IdentityRoleClaim<int>>>()
                .AddEntityFrameworkStores<BlogDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}