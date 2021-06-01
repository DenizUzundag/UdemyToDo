using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.BildirimDtos;
using YSKProje.ToDo.Entities.Concrete;


namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles ="Member")]
    [Area("MEmber")]
    public class BildirimController : Controller
    {
        private readonly IBildirimService _bildirimService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public BildirimController(IBildirimService bildirimService, UserManager<AppUser> userManager,IMapper mapper)
        {
            _bildirimService = bildirimService;
           _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "bildirim";
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
           
           
            return View(_mapper.Map<List<BildirimListDto>>(_bildirimService.GetirOkunmayanlar(user.Id)));
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
          var guncellenecekBildirim=  _bildirimService.GetirIdile(id);
            guncellenecekBildirim.Durum = true;
            _bildirimService.Guncelle(guncellenecekBildirim);
            return RedirectToAction("Index");
        }
    }
}
