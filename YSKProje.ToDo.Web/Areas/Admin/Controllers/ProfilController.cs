﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class ProfilController : BaseIdentityController
    {
       
        private readonly IMapper _mapper;

        public ProfilController(UserManager<AppUser> userManager,IMapper mapper):base(userManager)
        {

            _mapper = mapper;
        }
        public  async Task<IActionResult> Index()
        {
            TempData["Active"] = TempDataInfo.Profil;
        
           
            return View(_mapper.Map<AppUserListDto>(await GetirGirisYapanKullanici()));
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserListDto model,IFormFile resim)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekKullanici =  _userManager.Users.FirstOrDefault(I => I.Id == model.Id);
                if(resim != null)
                {
                    string uzanti = Path.GetExtension(resim.FileName);
                    string resimAd = Guid.NewGuid() + uzanti;

                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + resimAd);

                    using (var stream=new FileStream(path,FileMode.Create))
                    {
                       await resim.CopyToAsync(stream);
                    }
                    guncellenecekKullanici.Picture = resimAd;
                    guncellenecekKullanici.Name = model.Name;
                    guncellenecekKullanici.Surname = model.SurName;
                    guncellenecekKullanici.Email = model.Email;

                  var result = await _userManager.UpdateAsync(guncellenecekKullanici);
                    if(result.Succeeded)
                    {
                        TempData["Message"] = "Güncelleme işleminiz başarı ile gerçekleşti";
                        return RedirectToAction("Index");
                    }
                    HataEkle(result.Errors);
                }
            
            
            }
            return View(model);
     
        }
    }
}
