using AspNetCore.PluginManager;

namespace GSendService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            PluginManagerService.ConfigureServices(services);

            //services.AddControllersWithViews()
            //    .AddRazorRuntimeCompilation();

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

            app.UseWebSockets(new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromMinutes(2),
            });

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy();
            app.UseSession();
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
