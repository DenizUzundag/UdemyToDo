using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YSKProje.ToDo.Entities.Interfaces;

namespace YSKProje.ToDo.Entities.Concrete
{
   public class Gorev:ITablo
    {
       
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }

        public bool Durum { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
     
    }
}
