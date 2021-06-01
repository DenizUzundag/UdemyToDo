using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.GorevDtos;
using YSKProje.ToDo.DTO.DTOs.RaporDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = RoleInfo.Member)]
    [Area(AreaInfo.Member)]
    public class IsEmriController : BaseIdentityController
    {
      
        private readonly IGorevService _gorevService;
        private readonly IRaporService _raporService;
        private readonly IBildirimService _bildirimService;
        private readonly IMapper _mapper;

        public IsEmriController(IGorevService gorevService ,IRaporService raporService, UserManager<AppUser> userManager, IBildirimService bildirimService,IMapper mapper):base(userManager)
        {
            _gorevService = gorevService;
         
            _raporService = raporService;
            _bildirimService = bildirimService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var user = await GetirGirisYapanKullanici();
            TempData["Active"] = TempDataInfo.IsEmri;
          
         
            return View(_mapper.Map<List<GorevListAllDto>>(_gorevService.GetirTumTablolarla(I => I.AppUserId == user.Id && !I.Durum)));
        }
        public IActionResult EkleRapor(int id)
        {
            TempData["Active"] = TempDataInfo.IsEmri;
            var gorev= _gorevService.GetirAciliyetileId(id);

            RaporAddDto model = new RaporAddDto
            {
                GorevId = id,
                Gorev = gorev
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EkleRapor(RaporAddDto model)
        {
            if(ModelState.IsValid)
            {
                _raporService.Kaydet(new Rapor()
                {
                    GorevId = model.GorevId,
                    Detay = model.Detay,
                    Tanim =model.Tanim
                }) ;
                return RedirectToAction("Index");

            }

           var adminUserList = await _userManager.GetUsersInRoleAsync("Admin");
            var aktifKullanici = await GetirGirisYapanKullanici();
            foreach (var admin in adminUserList)
            {
                _bildirimService.Kaydet(new Bildirim
                {
                    Aciklama = $"{aktifKullanici.Name}{aktifKullanici.Surname} yeni bir rapor yazdı",
                    AppUseId = admin.Id
                });
            }



            return View(model);

        }

        public IActionResult GuncelleRapor(int id)
        {
            TempData["Active"] = TempDataInfo.IsEmri;
            var rapor = _raporService.GetirGorevileId(id);
            RaporUpdateDto model = new RaporUpdateDto
            {
                Id = rapor.Id,
                Tanim = rapor.Tanim,
                Detay = rapor.Detay,
                Gorev = rapor.Gorev,
                GorevId = rapor.GorevId
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult GuncelleRapor(RaporUpdateDto model)
        {
            if(ModelState.IsValid)
            {
               var guncellenecekRapor = _raporService.GetirIdile(model.Id);
  
                guncellenecekRapor.Tanim = model.Tanim;
                guncellenecekRapor.Detay = model.Detay;

                _raporService.Guncelle(guncellenecekRapor);
                return RedirectToAction("Index");

            }

            return View(model);
        }

        public async Task<IActionResult> TamamlaGorev(int gorevId)
        {
             var guncellenecekGorev=  _gorevService.GetirIdile(gorevId);
            guncellenecekGorev.Durum = true;
            _gorevService.Guncelle(guncellenecekGorev);

            var adminUserList = await _userManager.GetUsersInRoleAsync("Admin");
            var aktifKullanici = await GetirGirisYapanKullanici();
            foreach (var admin in adminUserList)
            {
                _bildirimService.Kaydet(new Bildirim
                {
                    Aciklama = $"{aktifKullanici.Name}{aktifKullanici.Surname} Görevi tamamladı.",
                    AppUseId = admin.Id
                });
            }

            return Json(null);
        }
    }
}
