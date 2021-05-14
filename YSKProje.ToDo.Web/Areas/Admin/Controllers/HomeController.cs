﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
   [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly IBildirimService _bidirimService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRaporService _raporService;
        public HomeController(IGorevService gorevService, IBildirimService bidirimService, UserManager<AppUser> userManager, IRaporService raporService)
        {
            _gorevService = gorevService;
            _bidirimService = bidirimService;
            _userManager = userManager;
            _raporService = raporService;
        }
        /*
         *atanmayı beklyen görev sayısı 
         *tamamlanmış görev sayısı
         *okunmamış bildirim sayısı
         *toplam yazılmış rapor sayısı
         */
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "anasayfa";
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
           ViewBag.AtanmayiBekleyenGorevSayisi = _gorevService.GetirAtanmayiBekleyenGorevSayisi();

            ViewBag.TamamlanmisGorevSayisi = _gorevService.GetirGorevTamamlanmis();

            ViewBag.OkunmamisBildirimSayisi = _bidirimService.GetirOkunmayanBildirimSayisiAppUserId(user.Id);

            ViewBag.ToplamRaporSayisi = _raporService.GetirRaporSayisi();
            return View();
        }
    }
}
