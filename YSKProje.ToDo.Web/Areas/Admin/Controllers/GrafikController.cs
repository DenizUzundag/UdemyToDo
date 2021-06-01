using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class GrafikController : Controller
    {
        private readonly IAppUserService _appUserService;
        public GrafikController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        //En çok görev tamamlamış 5 personel
        //en çok görev almış personel
        //selectAspNetUsers.UserName,Count(*) from AspNetUSers inner join Gorevler on AspNetUsers.Id=Gorev.AppUserId
        //where Gorevler.Durum=1 group by AspNetUsers.UserName count
        public IActionResult Index()
        {
            TempData["Active"] = TempDataInfo.Grafik;
            return View();
        }
        public IActionResult EnCokTamamlayan()
        {
           var JsonString=  JsonConvert.SerializeObject(_appUserService.GetirEnCokGorevTamamlamisPersoneller());
            return Json(JsonString);
        }
        public IActionResult EnCokCalisan()
        {
            var JsonString = JsonConvert.SerializeObject(_appUserService.GetirEnCokGorevdeCalisanPersoneller());
            return Json(JsonString);
        }
    }
}
