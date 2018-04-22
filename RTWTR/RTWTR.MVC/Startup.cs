using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RTWTR.Data;
using RTWTR.Service.External;
using RTWTR.Data.Models;
using RTWTR.Infrastructure.Mapping.Provider;
using System;
using RTWTR.Infrastructure.Contracts;
using RTWTR.Infrastructure;
using RTWTR.Service.Twitter.Contracts;
using RTWTR.Service.Twitter;

namespace RTWTR.MVC
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
            string connectionString = this.GetConnectionString();

            services.AddDbContext<RTWTRDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<RTWTRDbContext>()
                .AddDefaultTokenProviders();

            this.RegisterInfrastructure(services);

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<IVariableProvider, EnvironmentVariableProvider>();
            services.AddScoped<IHeaderGenerator, HeaderGenerator>();
            services.AddScoped<IEncoder, TokenEncoder>();
            services.AddScoped<IApiProvider, TwitterApiProvider_Deprecated>();
            services.AddScoped<ITwitterService, TwitterService>();

            services.AddMvc();
        }

        private void RegisterInfrastructure(IServiceCollection services)
        {
            services.AddSingleton<IMappingProvider, MappingProvider>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // This method assumes you have an environment variable named "ASPNETCORE_ENVIRONMENT"
        private string GetConnectionString()
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development"))
            {
                return Environment.GetEnvironmentVariable("rtwtr_dev").Replace(@"\\", @"\");                
            }

            return Environment.GetEnvironmentVariable("rtwtr").Replace(@"\\", @"\");
        }
    }
}
