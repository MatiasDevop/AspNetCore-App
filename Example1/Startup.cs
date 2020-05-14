using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Example1.Models;
using Example1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Example1
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("ConexionSQL")));
            services.AddMvc(options => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
                options.EnableEndpointRouting = false;
            }).AddXmlSerializerFormatters();// clasic mvc
            //services.AddMvcCore(options => options.EnableEndpointRouting = false); // this is on level more with improvement tha got net Core
            //services.AddSingleton<IFriendStore, MockFriendRepository>();
            services.AddScoped<IFriendStore, SQLFriendRepository>();

            services.AddIdentity<UserApplication, IdentityRole>(options => { }).AddErrorDescriber<ErrorCastellano>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Accounts/Login";
                options.LoginPath = "/Accounts/AccessDeneid";
            });
         
            
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions d = new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount = 2
                };

                app.UseDeveloperExceptionPage(d);
            }
            else if(env.IsProduction() || env.IsStaging())
            {
                // app.UseExceptionHandler("/Error");
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithRedirects("/Error/{0}");

            }

            app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseMvcWithDefaultRoute();// for setting our routing views and controller

            //app.Run(async (context) => // this is a Middleware 
            //{
            //    //throw new Exception("dasdsada");
            //    await context.Response.WriteAsync("Hellow");
            //});
            //app.UseMvc(routes =>// this is the way to config our routes like you want to
            //    {
            //        routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //        routes.MapRoute("", "{controller=Admin}/{action=CreateRol}");
            //    });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
