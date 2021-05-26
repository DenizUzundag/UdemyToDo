﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ProfilController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ProfilController(UserManager<AppUser> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public  async Task<IActionResult> Index()
        {
            TempData["Active"] = "Profil";
        
           
            return View(_mapper.Map<AppUserListDto>(await _userManager.FindByNameAsync(User.Identity.Name)));
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
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            
            
            }
            return View(model);
     
        }
    }
}
