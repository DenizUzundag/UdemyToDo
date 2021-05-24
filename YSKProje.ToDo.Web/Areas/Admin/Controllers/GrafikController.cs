using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class GrafikController : Controller
    {
        //En çok görev tamamlamış 5 personel
        //en çok görev almış personel
        //selectAspNetUsers.UserName,Count(*) from AspNetUSers inner join Gorevler on AspNetUsers.Id=Gorev.AppUserId
        //where Gorevler.Durum=1 group by AspNetUsers.UserName count
        public IActionResult Index()
        {
            TempData["Active"] = "grafik";
            return View();
        }
    }
}
