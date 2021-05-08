using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class IsEmriController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IGorevService _gorevService;
        private readonly IRaporService _raporService;

        public IsEmriController(IGorevService gorevService ,IRaporService raporService, UserManager<AppUser> userManager)
        {
            _gorevService = gorevService;
            _userManager = userManager;
            _raporService = raporService;
        }
        public async Task<IActionResult> Index()
        {
           var user =  await _userManager.FindByNameAsync(User.Identity.Name);
            TempData["Active"] = "isemri";
            var gorevler= _gorevService.GetirTumTablolarla(I => I.AppUserId == user.Id && !I.Durum);

            List<GorevListAllViewModel> models  = new List<GorevListAllViewModel>();

            foreach (var item in gorevler)
            {
                GorevListAllViewModel model = new GorevListAllViewModel();
                model.Id = item.Id;
                model.Aciklama = item.Aciklama;
                model.Aciliyet = item.Aciliyet;
                model.Ad = item.Ad;
                model.AppUser = item.AppUser;
                model.Raporlar = item.Raporlar;
                model.OlusturulmaTarihi = item.OlusturulmaTarihi;
                models.Add(model);


            }
          
            return View(models);
        }
        public IActionResult EkleRapor(int id)
        {
            TempData["Active"] = "isemri";
           var gorev= _gorevService.GetirAciliyetileId(id);

            RaporAddViewModel model = new RaporAddViewModel();
            model.GorevId = id;
            model.Gorev = gorev;
            return View(model);
        }
    }
}
