using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.TagHelpers
{
    [HtmlTargetElement("getirGorevAppUserId")]
    public class GorevAppUserIdTagHelper:TagHelper
    {
        private readonly IGorevService _gorevService;
        public GorevAppUserIdTagHelper(IGorevService gorevService)
        {
            _gorevService = gorevService;
        }
        public int AppUserId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
           List<Gorev>gorevler = _gorevService.GetirileAppUserId(AppUserId);
            int tamamlananGorevSayısı = gorevler.Where(I => I.Durum).Count();
            int ustundeCalistigiGorevSayisi = gorevler.Where(I => !I.Durum).Count();
            string htmlString = $"<strong>Tamamladığı görev sayısı: </strong>" +
                $"{tamamlananGorevSayısı}<br><strong>Üstünde Çalıştığı gorev sayısı: </strong>{ustundeCalistigiGorevSayisi}";
            output.Content.SetHtmlContent(htmlString);
        }

    }
}
