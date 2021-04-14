using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Member.Admin.Controllers
{
    [Area("Member")]
    [Authorize(Roles ="Member")]
    public class ProfilController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfilController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public  async Task<IActionResult> Index()
        {
            TempData["Active"] = "Profil";
           var appuser= await _userManager.FindByNameAsync(User.Identity.Name);
            AppUSerListViewModel model = new AppUSerListViewModel();
            model.Id = appuser.Id;
            model.Name = appuser.Name;
            model.SurName = appuser.Surname;
            model.Picture = appuser.Picture;
            model.Email = appuser.Email;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUSerListViewModel model,IFormFile resim)
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
