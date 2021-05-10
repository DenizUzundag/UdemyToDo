using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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
            //addScoped :bir tane örnek alýr. ilgili interface ait bir örnek alýr ve her zaman onu veriri
            //AddTransient: her seferinde yeni bir nesne örneði veriri
            //AddSingleton: istek kimden gelmiþ nerden gelmiþ önemi yok tek bir nesne örneði üretir.
            services.AddScoped<IGorevService, GorevManager>();//gorevServiceyi görünce gorevManageri örnekle
            services.AddScoped<IAciliyetService, AciliyetManager>();
            services.AddScoped<IRaporService, RaporManager>();
            services.AddScoped<IGorevDal, EfGorevRepository>();
            services.AddScoped<IRaporDal, EfRaporRepository>();
            services.AddScoped<IAciliyetDal, EfAciliyetRepository>();
            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IAppUserDal, EfAppUserRepository>();
            services.AddScoped<IDosyaService, DosyaManager>();
            services.AddScoped<IBildirimService, BildirimManager>();
            services.AddScoped<IBildirimDal, EfBildirimRepository>();
            services.AddDbContext<TodoContext>();
          
            services.AddIdentity<AppUser, AppRole>(opt=> {


                //password validasyon
                opt.Password.RequireDigit = false;//sayý içerme
                opt.Password.RequireUppercase = false;//büyük harf
                opt.Password.RequiredLength = 1;//default 6 biz 1
                opt.Password.RequireLowercase = false;//küçük harf
                opt.Password.RequireNonAlphanumeric = false; 
            }).
            
           AddEntityFrameworkStores<TodoContext>();
            services.AddControllersWithViews();
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "IsTakipCookie";
                opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(20);
                opt.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
                opt.LoginPath = "/Home/Index";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            IdentityInitializer.SeedDate(userManager, roleManager).Wait();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
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
