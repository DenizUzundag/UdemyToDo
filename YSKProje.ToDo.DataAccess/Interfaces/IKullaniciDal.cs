using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
    public interface IKullaniciDal
    {
        void Kaydet(Kullanici tablo);
        void Sil(Kullanici tablo);
        void Guncelle(Kullanici tablo);
        Kullanici GetirIdile(int id);
        List<Kullanici> GetirHepsi();
    }
}
