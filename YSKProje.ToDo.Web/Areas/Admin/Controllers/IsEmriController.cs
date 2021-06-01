using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.DTO.DTOs.GorevDtos;
using YSKProje.ToDo.Entities.Concrete;


namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class IsEmriController : Controller
    {
       
        private readonly IAppUserService _appUserService;
        private readonly IGorevService _gorevService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDosyaService _dosyaService;
        private readonly IBildirimService _bildirimService;
        private readonly IMapper _mapper;
        public IsEmriController(IAppUserService appUserService, IGorevService gorevService, UserManager<AppUser> userManager, IDosyaService dosyaService, IBildirimService bildirimService, IMapper mapper)
        {
            _appUserService = appUserService;
            _gorevService = gorevService;
            _userManager = userManager;
            _dosyaService = dosyaService;
            _bildirimService = bildirimService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            TempData["Active"] = "isemri";
         
           
            return View(_mapper.Map<List<GorevListAllDto>>(_gorevService.GetirTumTablolarla()));
        }
        public IActionResult AtaPersonel(int id,string s,int sayfa=1)
        {
            TempData["Active"] = "isemri";

            ViewBag.AktifSayfa = sayfa;
            //ViewBag.ToplamSAyfa = (int)Math.Ceiling((double)_appUserService.GetirAdminOlmayanlar().Count / 3);//toplam sayfa

            ViewBag.Aranan = s;
            var gorev = _gorevService.GetirAciliyetileId(id);


            var personeller =_mapper.Map<List<AppUserListDto>>(_appUserService.GetirAdminOlmayanlar(out int toplamSayfa,s,sayfa));
            ViewBag.ToplamSayfa = toplamSayfa;
            ViewBag.Personeller = personeller;
            

           
            return View(_mapper.Map<GorevListDto>(_gorevService.GetirAciliyetileId(id)));
        }

     //bildirim
        [HttpPost]
        public IActionResult AtaPersonel(PersonelGorevlendirDto model)
        {
           var guncellenecekGorev= _gorevService.GetirIdile(model.GorevId);
            guncellenecekGorev.AppUserId = model.PersonelId;

            _gorevService.Guncelle(guncellenecekGorev);

            _bildirimService.Kaydet(new Bildirim
            {
               AppUseId =model.PersonelId,
                Aciklama = $"{guncellenecekGorev.Ad} adlı iş için görevlendirildiniz."
            });


            return RedirectToAction("Index");
        }

        public IActionResult GorevlendirPersonel(PersonelGorevlendirDto model)
        {
            TempData["Active"] = "isemri";

            PersonelGorevlendirListDto personelGorevlendirModel = new PersonelGorevlendirListDto
            {
                AppUser = _mapper.Map<AppUserListDto>(_userManager.Users.FirstOrDefault(I => I.Id == model.PersonelId)),
                Gorev = _mapper.Map<GorevListDto>(_gorevService.GetirAciliyetileId(model.GorevId))
            };





            return View(personelGorevlendirModel);
        }
        public IActionResult Detaylandir(int id)
        {
            TempData["Active"] = "isemri";
           
            return View(_mapper.Map<GorevListAllDto>(_gorevService.GetirRaporlarileId(id)));
        }

        public IActionResult GetirExcel(int id)
        {

            return File( _dosyaService.AktarExcel(_gorevService.GetirRaporlarileId(id).Raporlar),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Guid.NewGuid() + ".xlsx");
        }
        public IActionResult GetirPdf(int id)
        {
            var path= _dosyaService.AktarPdf(_gorevService.GetirRaporlarileId(id).Raporlar);

            return File(path,"application/pdf", Guid.NewGuid()+".pdf");
        }
    }
}
