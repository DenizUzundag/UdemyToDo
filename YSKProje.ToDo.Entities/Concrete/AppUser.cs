using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Interfaces;

namespace YSKProje.ToDo.Entities.Concrete
{
    public class AppUser:IdentityUser<int>,ITablo//default string biz int primary key istiyoruz
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Picture { get; set; } = "defaulticon.png";
        // bir userın birden fazla gorev olabilir liste olarak gorev alacagız

        public List<Gorev> Gorevler { get; set; }
        public List<Bildirim> Bildirimler { get; set; }

    }
}
