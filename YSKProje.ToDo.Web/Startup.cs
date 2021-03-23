using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YSKProje.ToDo.Business.Concrete;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Context;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //addScoped :bir tane �rnek al�r. ilgili interface ait bir �rnek al�r ve her zaman onu veriri
            //AddTransient: her seferinde yeni bir nesne �rne�i veriri
            //AddSingleton: istek kimden gelmi� nerden gelmi� �nemi yok tek bir nesne �rne�i �retir.
            services.AddScoped<IGorevService, GorevManager>();//gorevServiceyi g�r�nce gorevManageri �rnekle
            services.AddScoped<IAciliyetService, AciliyetManager>();
            services.AddScoped<IRaporService, RaporManager>();
            services.AddScoped<IGorevDal, EfGorevRepository>();
            services.AddScoped<IRaporDal, EfRaporRepository>();
            services.AddScoped<IAciliyetDal, EfAciliyetRepository>();

            services.AddDbContext<TodoContext>();
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<TodoContext>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();//wwwroot

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{controller=Home}/{action=Index}/{id?}"

                    );


                endpoints.MapControllerRoute(
                    name:"defaukt",
                    pattern:"{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
