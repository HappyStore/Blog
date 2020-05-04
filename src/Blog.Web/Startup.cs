using System;
using Blog.DataAccess.Infrastructure;
using Blog.Handlers.Infrastructure;
using Blog.Services.Authentication;
using Blog.Services.Infrastructure;
using Blog.Web.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtTokenOptions = Configuration
                .GetSection("Authentication")
                .Get<JwtTokenOptions>();
            
            services
                .AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    });
                });

            services
                .AddMvc(options => { options.Filters.Add(new ProducesAttribute("application/json")); })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
            
            
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtTokenOptions.Issuer,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Convert.FromBase64String(jwtTokenOptions.SigningSecret)),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30)
            };
            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy(
                        AuthorizationPolicies.SuperAdmin,
                        policy => policy
                            .RequireAuthenticatedUser()
                            .RequireRole(AuthorizationPolicies.SuperAdmin)
                    );
                    options.AddPolicy(
                        AuthorizationPolicies.Administrator,
                        policy => policy
                            .RequireAuthenticatedUser()
                            .RequireRole(AuthorizationPolicies.Administrator)
                    );
                    options.AddPolicy(
                        AuthorizationPolicies.Moderator,
                        policy => policy
                            .RequireAuthenticatedUser()
                            .RequireRole(AuthorizationPolicies.Moderator)
                    );
                })
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.ClaimsIssuer = jwtTokenOptions.Issuer;
                    options.TokenValidationParameters = tokenValidationParameters;
                });
            
            services.AddControllers();
            services.AddMediator();
            services
                .AddBlogDatabase()
                .AddBlogServices();
            
            
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}