using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
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
            return View(model);
        }
    }
}
