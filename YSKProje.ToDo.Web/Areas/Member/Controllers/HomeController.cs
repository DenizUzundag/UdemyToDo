using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Area("Member")]

    [Authorize(Roles = "Member")]
    public class HomeController : Controller
    {
        private readonly IRaporService _raporService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IGorevService _gorevService;

        public HomeController(IRaporService raporService, UserManager<AppUser> userManager, IGorevService gorevService)
        {
            _raporService = raporService;
            _userManager = userManager;
            _gorevService = gorevService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "anasayfa";
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.RaporSayisi = _raporService.GetirRaporSayisiileAppUserId(user.Id);
            ViewBag.TamamlananGorevSayisi = _gorevService.GetirGorevSayisiTamamlananileAppUserId(user.Id);
            return View();
        }
    }
}
