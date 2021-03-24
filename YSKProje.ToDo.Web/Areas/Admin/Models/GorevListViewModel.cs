using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Areas.Admin.Models
{
    public class GorevListViewModel
    {
        public int Id { get; set; }

        public string Ad { get; set; }
        public string Aciklama { get; set; }

        public bool Durum { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        public int AciliyetId { get; set; }//zorunlu
        public Aciliyet Aciliyet { get; set; }

    }
}
