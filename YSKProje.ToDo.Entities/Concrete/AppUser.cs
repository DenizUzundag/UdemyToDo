using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.Entities.Concrete
{
    public class AppUser:IdentityUser<int>//default string biz int primary key istiyoruz
    {
    }
}
