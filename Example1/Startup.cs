using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            services.AddMvc(options => options.EnableEndpointRouting = false);// clasic mvc
            //services.AddMvcCore(options => options.EnableEndpointRouting = false); // this is on level more with improvement tha got net Core
            //services.AddSingleton<IFriendStore, MockFriendRepository>();
            services.AddScoped<IFriendStore, SQLFriendRepository>();
           
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

                app.UseDeveloperExceptionPage();
            }
            else if(env.IsProduction() || env.IsStaging())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();// for setting our routing views and controller

            //app.Run(async (context) => // this is a Middleware 
            //{
            //    //throw new Exception("dasdsada");
            //    await context.Response.WriteAsync("Hellow");
            //});
            //app.UseMvc();
            //    (routes =>// this is the way to config our routes like you want to
            //{
            //    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
