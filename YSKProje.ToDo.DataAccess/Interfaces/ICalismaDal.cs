using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
    public interface ICalismaDal
    {
        void Kaydet(Calisma tablo);
        void Sil(Calisma tablo);
        void Guncelle(Calisma tablo);
        Calisma GetirIdile(int id);
        List<Calisma> GetirHepsi();
    }
}
