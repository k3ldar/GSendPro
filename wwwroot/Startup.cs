﻿using AspNetCore.PluginManager;

using Breadcrumb.Plugin;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace gsend.pro
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            PluginManagerService.ConfigureServices(services);

            services.AddControllersWithViews();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication("DefaultAuthSchemeName")
                .AddCookie("DefaultAuthSchemeName", options =>
                {
                    options.AccessDeniedPath = "/Error/AccessDenied";
                    options.LoginPath = "/Login/";
                });

            services.AddSession();
            services.AddDistributedMemoryCache();

            services.AddMvc(
                    option => option.EnableEndpointRouting = false
                )
                .ConfigurePluginManager()
                .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            PluginManagerService.Configure(app);

            app.UseHsts();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseBreadcrumbs();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            }).UsePluginManager()
            ;
        }
    }
}
