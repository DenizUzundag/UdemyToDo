using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Web.Models;

namespace YSKProje.ToDo.Web.ViewComponents
{
    public class Urunler: ViewComponent
    {
        //bir viewComp. çalıştırabilmemiz için  Invoke metodu ayağa kaldırılmalı

        public IViewComponentResult Invoke()
        {
            //default adında bir view aradı ve bulamadı
            return View("Yeni",new List<MusteriViewModel>()
            {
                new MusteriViewModel(){Ad="deniz1"},
                 new MusteriViewModel(){Ad="deniz2"},
                  new MusteriViewModel(){Ad="deniz3"}
            });
        }
    }
}
