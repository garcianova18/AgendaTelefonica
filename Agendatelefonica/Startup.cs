using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Agendatelefonica.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Agendatelefonica.Controllers;
using Agendatelefonica.Services;

namespace Agendatelefonica
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

            services.AddScoped<IRepositoryElectromecanica, RepositoryElectromecanica>();
            services.AddScoped(typeof(IRepositoryGenerico<>), typeof(RepositoryGenerico<>));

            services.AddDbContext<AgendatelefonicaContext>(opciones => opciones.UseSqlServer(Configuration.GetConnectionString("connexion")));

            services.AddAutoMapper(typeof(Startup));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(opciones =>
               {

                   opciones.LoginPath = "/Acceso/Index";
                   // opciones.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                   opciones.AccessDeniedPath = "/Acceso/Restringido";


               });

            services.AddSignalR();

           
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Acceso}/{action=Index}/{id?}");

                endpoints.MapHub<agendaHub>("/agendaHud");
            });

           
        }
    }
}
