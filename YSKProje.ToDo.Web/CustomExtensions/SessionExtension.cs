using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.CustomExtensions
{
    public static class SessionExtension
    {

        //artık bir session nesnesinin içine obje atabileceğiz
        public static void SetObject(this ISession session,string key, object deger)
        {
           string gelenData = JsonConvert.SerializeObject(deger);
            session.SetString(key, gelenData);

        }
        public static Deniz GetObject<Deniz>(this ISession session, string key)
            where Deniz : class, new()
        {

            string gelenData= session.GetString(key);
            if(gelenData !=null )
            {
                return JsonConvert.DeserializeObject<Deniz>(gelenData);
            }
            return null;
        }
    }
}
