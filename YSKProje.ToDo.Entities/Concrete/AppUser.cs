using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.Entities.Concrete
{
    public class AppUser:IdentityUser<int>//default string biz int primary key istiyoruz
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        // bir userın birden fazla gorev olabilir liste olarak gorev alacagız

        public List<Gorev> Gorevler { get; set; }
    }
}
