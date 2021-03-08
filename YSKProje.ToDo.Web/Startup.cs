using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Web.Middlewares;
using Microsoft.AspNetCore.Routing.Constraints;
using YSKProje.ToDo.Web.Constraints;

namespace YSKProje.ToDo.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //bu metot ilgili sisteme eklemek istediðim middleware burada belirtiyoruz.
            services.AddSession();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //ilgili middleware kullanýyoruz


            //uygulama development modda(bunu properties dosyasýnýn içindeki launchSettings.jsondan anlýyor) ise bana hata sayfalarýný göster
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //useRouting  ysk.com.tr/Index.asp  NetCore'da bunun anlamlý hale gelmesi için routingi kullanmamýz gerekiyor..
            //tek baþýna olunca wwwrootu dýþarý açar . parametreli kullanýmýda var
            app.UseRouting();
      

            //bunu dediðimizde wwwroot adýndaki bir dosyayý dýþarý açarýz
            app.UseStaticFiles();
            app.UseCustomStaticFile();
            app.UseSession();
            //app.UseStatusCodePages();
         
            app.UseStatusCodePagesWithReExecute("/Home/PageError", "?code={0}");
            //Routing
            app.UseEndpoints(endpoints =>
            {


                //ne kadar area tanýmlarsak tanýmlayalým hepsini karþýlayacak
                endpoints.MapControllerRoute(
                    name: "area",
                    pattern:"{area}/{controller=Home}/{action=Index}/{id?}"
                    );




                //endpoints.MapAreaControllerRoute(
                //    name:"areaAdmin",
                //    areaName:"Admin",
                //    pattern:"{area}/{controller}/{action}"


                //    );



                endpoints.MapControllerRoute(
                    name:"programlamaRoute",
                    pattern :"programlama/{dil}",
                    defaults: new { controller ="Home", action="Index" },
                    constraints: new {dil = new Programlama()}


                    );

                endpoints.MapControllerRoute(
                   name: "kisi",
                   pattern: "kisiler",
                   defaults:  new { controller = "Home" ,action = "Index"}
                   );


                endpoints.MapControllerRoute(
                    name: "default",
                    //bu patterna uygun bir istek gelirse bu endpoint çalýþýp ilgili controller ve actionu çalýþtýrýcak
                    //uygun bir istek gelmezse 404 notfound hatasý alýrdýk
                    //? opsiyonellik kattý bir kýsýtlayýcýdýr
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    //id integer deðer olmak zorunda
                    //constraints: new {id = new IntRouteConstraint()}
                    //yada{id:int yada alpha:ing karakterler yada :bool (true-false)}
                    );

               
            });
        }
    }
}
