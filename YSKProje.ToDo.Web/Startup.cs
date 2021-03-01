using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //bu metot ilgili sisteme eklemek istedi�im middleware burada belirtiyoruz.

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //ilgili middleware kullan�yoruz


            //uygulama development modda(bunu properties dosyas�n�n i�indeki launchSettings.jsondan anl�yor) ise bana hata sayfalar�n� g�ster
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //useRouting  ysk.com.tr/Index.asp  NetCore'da bunun anlaml� hale gelmesi i�in routingi kullanmam�z gerekiyor..
            app.UseRouting();

            
            app.UseEndpoints(endpoints =>
            {

             
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}"
                    );
            });
        }
    }
}
