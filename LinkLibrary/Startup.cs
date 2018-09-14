using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LinkLibrary.Entities;
using LinkLibrary.Models;
using LinkLibrary.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.WebSockets;
using System.Threading;
using LinkLibrary.Helpers;

namespace LinkLibrary
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<LinkLibraryContext>(o => 
                    o.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=LinkLibraryDb;Trusted_Connection=True;"));

            services.AddIdentity<IdentityUser<int>, IdentityRole<int>>().AddEntityFrameworkStores<LinkLibraryContext>();
            services.AddMvc();
            services.AddScoped<ILinkLibraryRepository, LinkLibraryRepository>();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages(async context => {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    response.StatusCode == (int)HttpStatusCode.Forbidden ||
                    response.StatusCode == (int)HttpStatusCode.NotFound)
                    response.Redirect("/Login");
            });
            app.UseStaticFiles();
            app.UseSession();
            app.UseIdentity();
            app.UseWebSockets();
            app.UseMiddleware<SocketMiddleware>();
            

            AutoMapper.Mapper.Initialize(cfg =>
           {
               cfg.CreateMap<Link, LinkDto>();
               cfg.CreateMap<LinkToAddDto, Link>();
               cfg.CreateMap<Link, LinkToGetDto>();
               cfg.CreateMap<LinkToAddDto, Link>();
               cfg.CreateMap<Link, LinkViewDto>();
           });
            

            app.UseMvc();


        }
    }
}
