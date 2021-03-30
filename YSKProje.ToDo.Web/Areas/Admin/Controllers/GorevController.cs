using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class GorevController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly IAciliyetService _aciliyetService;
        public GorevController(IGorevService gorevService, IAciliyetService aciliyetService)
        {
            _gorevService = gorevService;
            _aciliyetService = aciliyetService;

        }
        public IActionResult Index()
        {
            TempData["Active"] = "gorev";
            List<Gorev> gorevler =  _gorevService.GetirAciliyetIleTamamlanmayan();

            //bunları daha sonra mapper kütüphanelerini kullanaarak refactoring yapacagız
            List<GorevListViewModel> models = new List<GorevListViewModel>();
            foreach (var item in gorevler)
            {
                GorevListViewModel model = new GorevListViewModel
                {
                    Aciklama = item.Aciklama,
                    Aciliyet=item.Aciliyet,
                    AciliyetId =item.AciliyetId,
                    Ad=item.Ad,
                    Durum =item.Durum,
                    Id=item.Id,
                    OlusturulmaTarihi =item.OlusturulmaTarihi

                };
                models.Add(model);
            }

            return View(models);
        }

        public IActionResult EkleGorev()
        {
            TempData["Active"] = "gorev";

            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim");  //Tanım gösterilecek, ıd ve tanımla bu işi yapacağız
            return View(new GorevAddViewModel());
        }

        [HttpPost]
        public IActionResult EkleGorev(GorevAddViewModel model)
        {
            if(ModelState.IsValid)
            {
                _gorevService.Kaydet(new Gorev
                {
                    Aciklama=model.Aciklama,
                    Ad=model.Ad,
                    AciliyetId=model.AciliyetId,
                    


                });
                return RedirectToAction("Index");
            }
           
            return View(model);

        }

        public IActionResult GuncelleGorev(int id)
        {
           var gorev = _gorevService.GetirIdile(id);
            GorevUpdateViewModel model = new GorevUpdateViewModel
            {
                Id = gorev.Id,
                Aciklama = gorev.Aciklama,
                AciliyetId = gorev.AciliyetId,
                Ad = gorev.Ad
            };
            TempData["Active"] = "gorev";

            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim",gorev.AciliyetId);  //Aciliyet seçili olarak gelicek
            return View(model);
        }
        [HttpPost]
        public IActionResult GuncelleGorev(GorevUpdateViewModel model)
        {
            if(ModelState.IsValid)
            {
                _gorevService.Guncelle(new Gorev()
                {
                    Id=model.Id,
                    Aciklama=model.Aciklama,
                    AciliyetId=model.AciliyetId,
                    Ad=model.Ad

                });
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult SilGorev(int id)
        {
            _gorevService.Sil(new Gorev { Id = id });
            return RedirectToAction("Index");

        }

    }
}
