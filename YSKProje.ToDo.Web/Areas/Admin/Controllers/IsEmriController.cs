using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class IsEmriController : Controller
    {
       
        private readonly IAppUserService _appUserService;
        private readonly IGorevService _gorevService;
        public IsEmriController(IAppUserService appUserService, IGorevService gorevService)
        {
            _appUserService = appUserService;
            _gorevService = gorevService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = "isemri";
            //var model = _appUserService.GetirAdminOlmayanlar();
            List<Gorev>gorevler = _gorevService.GetirTumTablolarla();//liste olarak bir gorev listesi dönecek
            List<GorevListAllViewModel> models = new List<GorevListAllViewModel>();
            foreach (var item in gorevler)
            {
                GorevListAllViewModel model = new GorevListAllViewModel();
                model.Id = item.Id;
                model.Aciklama = item.Aciklama;
                model.Aciliyet = item.Aciliyet;
                model.Ad = item.Ad;
                model.AppUser = item.AppUser;
                model.OlusturulmaTarihi = item.OlusturulmaTarihi;
                model.Raporlar = item.Raporlar;
                models.Add(model);
            }
            return View(models);
        }
        public IActionResult AtaPersonel(int id,string s,int sayfa=1)
        {
            TempData["Active"] = "isemri";

            ViewBag.AktifSayfa = sayfa;
            //ViewBag.ToplamSAyfa = (int)Math.Ceiling((double)_appUserService.GetirAdminOlmayanlar().Count / 3);//toplam sayfa

            ViewBag.Aranan = s;
            int toplamSayfa;
            var gorev = _gorevService.GetirAciliyetileId(id);


            var personeller = _appUserService.GetirAdminOlmayanlar(out toplamSayfa,s,sayfa);
            ViewBag.ToplamSayfa = toplamSayfa;
            List<AppUSerListViewModel> appUserListmodel = new List<AppUSerListViewModel>();
            foreach (var item in personeller)
            {
                AppUSerListViewModel model = new AppUSerListViewModel();
                model.Id = item.Id;
                model.Name = item.Name;
                model.SurName = item.Surname;
                model.Email = item.Email;
                model.Picture = item.Picture;
                appUserListmodel.Add(model);
            }
            ViewBag.Personeller = appUserListmodel;

            GorevListViewModel gorevmodel = new GorevListViewModel();
            gorevmodel.Id = gorev.Id;
            gorevmodel.Ad = gorev.Ad;
            gorevmodel.Aciklama = gorev.Aciklama;
            gorevmodel.Aciliyet = gorev.Aciliyet;
            gorevmodel.OlusturulmaTarihi = gorev.OlusturulmaTarihi;
            return View(gorevmodel);
        }
    }
}
