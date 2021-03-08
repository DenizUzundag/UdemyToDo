using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Web.CustomExtensions;
using YSKProje.ToDo.Web.CustomFilters;
using YSKProje.ToDo.Web.Models;

namespace YSKProje.ToDo.Web.Controllers
{
    // [Route("kisiler/[action]")]//bu şekide bir route tanımlarsak startupda yazdığımız endpointler geçersiz oluyor hepsini eziyor
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            //ViewBag , tempdata viewdata model

            ViewBag.Isım = "Deniz";
            TempData["Isim"] = "Deniz";
            ViewData["Isim"] = "Deniz";

            SetCookie();
            ViewBag.Cookie = GetCookie();

            //GetCookie();
            //ViewBag.Cookie = GetSession();

            return View();
        }
        public IActionResult KayitOl()
        {
            return View();
        }

        [AdDenizOlamaz]
        [HttpPost]
        public IActionResult Kayitol2(KullaniciKayitViewModel model)

        {
            //string ad= HttpContext.Request.Form["Ad"].ToString();
            //ViewBag.Ad = ad;
            if (ModelState.IsValid)
            {

            }
            ModelState.AddModelError(nameof(KullaniciKayitViewModel.Ad), "ad alanı gereklidir");
            ModelState.AddModelError("", "Modelle ilgili hata");  //bu şekide istediğimiz propertye istediğimiz error message geçer ve kullanıcıya gösterebiliriz

            return View("KayitOl", model);
        }
        public void SetCookie()
        {
            HttpContext.Response.Cookies.Append("kisi", "deniz", new Microsoft.AspNetCore.Http.CookieOptions()
            {
                Expires = DateTime.Now.AddDays(20),//bugünden sonra 20 gün yaşıyacak
                HttpOnly = true,//javascripte kapatıyoruz.
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,//default lax bu cookie dışarı açmış oluyoruz
                //Secure = true
            }); ;
        }
        public string GetCookie()
        {
            return HttpContext.Request.Cookies["kisi"];
        }

        public void setSession()
        {
            //HttpContext.Session.SetString("kisi", "deniz");
            HttpContext.Session.SetObject("kisi", new KullaniciKayitViewModel()
            {
                Ad = "Deniz",
                Soyad = "Uzundağ"
            });
        }

        public KullaniciKayitViewModel GetSession()
        {
            return HttpContext.Session.GetObject<KullaniciKayitViewModel>("kisi");
        }

        public IActionResult PageError(int code)
        {
            ViewBag.Code = code;
            if (code == 404)
            {
                ViewBag.ErrorMessage = "Sayfa bulunamadı";
            }
            return View();
        }

        public IActionResult Hata()
        {
            throw new Exception("hata oluştu");
        }
        public IActionResult Error()
        {
            //hatayı yakalıyoruz bu sayede hatayı loglaya biliriz
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            CustomLogger.NLogLogger nlogLogger = new CustomLogger.NLogLogger();
            nlogLogger.LogWithNLog($"hatanın oluştuğu yer{exceptionHandlerPathFeature.Path} \n hata mesajı : " +
                $"{exceptionHandlerPathFeature.Error.Message}\n stack trace: {exceptionHandlerPathFeature.Error.StackTrace} ");
            ViewBag.Path = exceptionHandlerPathFeature.Path;//hatanın yeri
            ViewBag.MEssage = exceptionHandlerPathFeature.Error.Message;

            return View();
        }
    }
}
