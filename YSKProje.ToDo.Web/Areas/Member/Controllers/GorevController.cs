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
using YSKProje.ToDo.Entities.Concrete;
namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles ="Member")]
    [Area("Member")]
    public class GorevController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GorevController(IGorevService gorevService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _gorevService = gorevService;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int aktifSayfa=1)
        {

            TempData["Active"] = "gorev";
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var gorevler = _mapper.Map<List<GorevListAllDto>>(_gorevService.GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, user.Id, aktifSayfa));

            ViewBag.ToplamSayfa = toplamSayfa;
            ViewBag.AktifSayfa = aktifSayfa;

            
            return View(gorevler);
        }
    }
}
