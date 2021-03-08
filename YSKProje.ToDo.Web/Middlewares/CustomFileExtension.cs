using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Middlewares
{
    public static class CustomFileExtension
    {
        public static void UseCustomStaticFile(this IApplicationBuilder app)
        {
            /*
             * FileProvider = new PhysicalFileProvider(Path.Combine
             *(Directory.GetCurrentDirectory(),"node_modules")),
             *RequestPath="/content"
             */
        }
    }
}
