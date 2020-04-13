using Blog.DataAccess.Infrastructure;
using Blog.Services.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                })
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllers();
            services.AddBlogDatabase();
            services.AddMediator();
        }
        
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Map("/api", api =>
            {
                if (env.IsDevelopment())
                    api.UseDeveloperExceptionPage();
                else
                    api.UseHttpsRedirection();

                app.UseRouting();

                app.UseCors();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            });
        }
    }
}