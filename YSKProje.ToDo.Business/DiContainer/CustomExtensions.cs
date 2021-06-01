using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Business.Concrete;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.DataAccess.Interfaces;

namespace YSKProje.ToDo.Business.DiContainer
{
    public static class CustomExtensions
    {
        public static void AddContainerWithDepencies(this IServiceCollection services)
        {
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
        }
    }
}
