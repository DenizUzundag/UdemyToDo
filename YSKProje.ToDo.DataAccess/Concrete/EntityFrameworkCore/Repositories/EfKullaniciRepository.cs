using System;
using System.Collections.Generic;
using System.Linq;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Context;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfKullaniciRepository : IKullaniciDal
    {
        public List<Kullanici> GetirHepsi()
        {
            using var context = new TodoContext();
            return context.Kullanicilar.ToList();
        }

        public Kullanici GetirIdile(int id)
        {
            using var context = new TodoContext();
            return context.Kullanicilar.Find(id);
        }

        public void Guncelle(Kullanici tablo)
        {
            using var context = new TodoContext();

            //biz bir entity nesne örneğini add update veya delete metotlarıyla gönderdiğimizde arka tarafta örnek olarak güncelleme
            //işlemi yapılıyorsa stateini modified olarak işaretler
            context.Kullanicilar.Update(tablo);
            context.SaveChanges();
        }

        public void Kaydet(Kullanici tablo)
        {
            using var context = new TodoContext();
            context.Kullanicilar.Add(tablo);
            context.SaveChanges();
        }

        public void Sil(Kullanici tablo)
        {
            using var context = new TodoContext();
            context.Kullanicilar.Remove(tablo);
            context.SaveChanges();

        }
    }
}
