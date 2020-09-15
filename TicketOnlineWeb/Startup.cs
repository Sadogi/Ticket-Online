using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoTools;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketOnline.Forms;
using TicketOnline.Interfaces;
using TicketOnline.Models.Data;
using TicketOnline.Models.Data.Repositories;
using TicketOnlineWeb.Infrastructure;

namespace TicketOnlineWeb
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
            services.AddControllersWithViews();            

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddHttpContextAccessor();
            services.AddTransient<ISessionManager, SessionManager>();
            services.AddScoped<ICryptoRSA, CryptoRSA>(c => new CryptoRSA());
            services.AddDistributedMemoryCache();
            services.AddSingleton<SecurityRequester>();
            services.AddSingleton<ApiRequester>();
            services.AddSingleton<ApiTokenRequester>();
            services.AddSingleton(p => new string("http://localhost:51049/api/"));
            services.AddSingleton<IAuthRepository<RegisterForm, LoginForm, User>, AuthRequester>();
            //services.AddSingleton<IUserEventRepository<Event>, EventRequester>();
            services.AddSingleton<IUserEventRepository<Event>, EventRequester>();
            services.AddSingleton<IUserEventRepository<User>, UserRequester>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
