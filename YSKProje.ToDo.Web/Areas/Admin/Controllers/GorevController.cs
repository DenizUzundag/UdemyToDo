using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.GorevDtos;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class GorevController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly IAciliyetService _aciliyetService;
        private readonly IMapper _mapper;
        public GorevController(IGorevService gorevService, IAciliyetService aciliyetService, IMapper mapper)
        {
            _gorevService = gorevService;
            _aciliyetService = aciliyetService;
            _mapper = mapper;

        }
        public IActionResult Index()
        {
            TempData["Active"] = "gorev";
          

            return View(
            _mapper.Map<List<GorevListDto>>(_gorevService.GetirAciliyetIleTamamlanmayan()));
        }

        public IActionResult EkleGorev()
        {
            TempData["Active"] = "gorev";

            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim");  //Tanım gösterilecek, ıd ve tanımla bu işi yapacağız
            return View(new GorevAddDto());
        }

        [HttpPost]
        public IActionResult EkleGorev(GorevAddDto model)
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
        
            TempData["Active"] = "gorev";
            
            var gorev = _gorevService.GetirIdile(id);
            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim",gorev.AciliyetId);  //Aciliyet seçili olarak gelicek
            return View(_mapper.Map<GorevUpdateDto>(gorev));
        }
        [HttpPost]
        public IActionResult GuncelleGorev(GorevUpdateDto model)
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
            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim", model.AciliyetId);
            return View(model);
        }

        public IActionResult SilGorev(int id)
        {
            _gorevService.Sil(new Gorev { Id = id });
            return RedirectToAction("Index");

        }

    }
}
