using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.TagHelpers
{
    [HtmlTargetElement("Deniz")]
    public class Paragraf : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //string data;
            //tagbuilder
            //stringbuilder


            //p etiketi içine html datası bastı
            //<p><b>Deniz Uzundağ</b></p>
            // var tagBuilder = new TagBuilder("p");
            //tagBuilder.InnerHtml.AppendHtml("<b>Deniz Uzundağ</b>");
            // output.Content.SetHtmlContent(tagBuilder);
           


            //var stringBuilder = new StringBuilder();
            //appendFormat kullanılabilir ("<p><b>{0}</b></p>","deniz uzundag")
            //stringBuilder.Append("<p>");
            // output.Content.SetHtmlContent(stringBuilder.ToString());


            string data = string.Empty;
            data = "<p> <b>" + "deniz uzundağ" + "</b></p>";
            output.Content.SetHtmlContent(data);//htmlolarak yaz 
      
        }
    }
}
