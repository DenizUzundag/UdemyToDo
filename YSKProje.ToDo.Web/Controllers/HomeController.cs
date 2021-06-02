using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;

namespace YSKProje.ToDo.Web.Controllers
{
    public class HomeController : BaseIdentityController
    {

     
        private readonly SignInManager<AppUser> _signInManager;
      
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager):base(userManager)
        {
           
      
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GirisYap(AppUserSignInDto model)
        {
            if(ModelState.IsValid)
            {
                //user var mı yok mu
                var user = await _userManager.FindByNameAsync(model.UserName);
                if(user != null)
                {
                    //parola ile giriş kontolü
                var identityResult =   await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                    if(identityResult.Succeeded)
                    {
                        var roller = await _userManager.GetRolesAsync(user);
                       if( roller.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home", new { area = "Member" });
                        }
                    }
                    
                    
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
            }
            return View("Index",model);
           
        }

        public IActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KayitOl(AppUserAddDto model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //bir rol ata
                    var addroleResult = await _userManager.AddToRoleAsync(user, "Member");
                    if (addroleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    HataEkle(addroleResult.Errors);
                }
                HataEkle(result.Errors);
               
            }
            return View(model);
        }
        public async Task<IActionResult> CikisYap()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Index");

        }
        public IActionResult StatusCode(int? code)
        {
            if(code==404)
            {
                ViewBag.Code = code;
                ViewBag.Message = "Sayfa Bulunamadı";
            }
            
            return View();
        }
    }
}
