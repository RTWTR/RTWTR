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
using AutoMapper;
using RTWTR.Infrastructure.Contracts;
using RTWTR.Infrastructure;
using RTWTR.Service.Data;
using RTWTR.Service.Data.Contracts;
using RTWTR.Service.Twitter.Contracts;
using RTWTR.Service.Twitter;
using RTWTR.Data.Access;
using RTWTR.Data.Access.Contracts;

namespace RTWTR.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.RegisterData(services);

            this.RegisterIdentity(services);

            this.RegisterServices(services);

            this.RegisterInfrastructure(services);
        }

        private void RegisterData(IServiceCollection services)
        {
            string connectionString = this.GetDbConnectionString();

            services.AddDbContext<RTWTRDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddScoped<ISaver, Saver>();
        }

        private void RegisterIdentity(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<RTWTRDbContext>()
                .AddDefaultTokenProviders();


            if (this.Environment.IsDevelopment())
            {
                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 0;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(1);
                    options.Lockout.MaxFailedAccessAttempts = 999;
                });
            }
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IVariableProvider, EnvironmentVariableProvider>();
            services.AddScoped<IHeaderGenerator, HeaderGenerator>();
            services.AddScoped<IEncoder, TokenEncoder>();
            services.AddScoped<IApiProvider, TwitterApiProvider>();
            services.AddScoped<ITwitterService, TwitterService>();
            services.AddScoped<IJsonProvider, JsonProvider>();
            services.AddTransient<ITwitterUserService, TwitterUserService>();
            services.AddTransient<ITweetService, TweetService>();
            services.AddTransient<ICollectionService, CollectionService>();
        }

        private void RegisterInfrastructure(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddScoped<IMappingProvider, MappingProvider>();

            services.AddMvc();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (this.Environment.IsDevelopment())
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
        private string GetDbConnectionString()
        {
            if (System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development"))
            {
                return System.Environment.GetEnvironmentVariable("rtwtr_dev").Replace(@"\\", @"\");
            }

            return System.Environment.GetEnvironmentVariable("rtwtr").Replace(@"\\", @"\");
        }
    }
}
