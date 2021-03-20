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


        //bir gorev varsa bir kişi yapar bunu
        public int? AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        //aciliyet tek olan
        public int AciliyetId { get; set; }//zorunlu
        public Aciliyet Aciliyet { get; set; }

        //Rapor
        public List<Rapor> Raporlar { get; set; }
    }
}
