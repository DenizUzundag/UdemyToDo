using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Web.Models;

namespace YSKProje.ToDo.Web.CustomFilters
{
    public class AdDenizOlamaz : ActionFilterAttribute
    {
        //action çalışmadan önce ..
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dictionaryGelen = context.ActionArguments.Where(I => I.Key == "model").FirstOrDefault();
            var model =(KullaniciKayitViewModel)dictionaryGelen.Value;
            if(model.Ad.ToLower()=="deniz")
            {
                context.Result = new RedirectResult("\\Home\\Error");
            }
            base.OnActionExecuting(context);
        }

    }
}
