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
            #region Development Only
            services.AddDbContext<RTWTRDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Development")));
            #endregion

            #region Production
            //services.AddDbContext<RTWTRDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("Production")));
            #endregion

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<RTWTRDbContext>()
                .AddDefaultTokenProviders();

            this.RegisterInfrastructure(services);

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

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
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
