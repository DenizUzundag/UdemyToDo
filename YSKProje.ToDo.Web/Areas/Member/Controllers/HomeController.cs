using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Area("Member")]

    [Authorize(Roles = "Member")]
    public class HomeController : BaseIdentityController
    { 
        private readonly IRaporService _raporService;
   
        private readonly IGorevService _gorevService;
        private readonly IBildirimService _bildirimService;

        public HomeController(IRaporService raporService, UserManager<AppUser> userManager, IGorevService gorevService, IBildirimService bildirimService):base(userManager)
        {
            _raporService = raporService;
        
            _gorevService = gorevService;
            _bildirimService = bildirimService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "anasayfa";
            var user = await GetirGirisYapanKullanici();
            ViewBag.RaporSayisi = _raporService.GetirRaporSayisiileAppUserId(user.Id);
            ViewBag.TamamlananGorevSayisi = _gorevService.GetirGorevSayisiTamamlananileAppUserId(user.Id);
            ViewBag.TamamlanmayanGorevSaysii = _gorevService.GetirTamamlanmyanGorevSayisi(user.Id);
            ViewBag.OkunmamısBildirimSayisi = _bildirimService.GetirOkunmayanBildirimSayisiAppUserId(user.Id);
            return View();
        }
    }
}
