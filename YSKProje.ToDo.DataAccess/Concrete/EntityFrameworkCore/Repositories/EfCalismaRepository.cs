using System;
using System.Collections.Generic;
using System.Linq;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Context;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCalismaRepository : ICalismaDal
    {
        public List<Calisma> GetirHepsi()
        {
            using var context = new TodoContext();
            return context.Calismalar.ToList();
        }

        public Calisma GetirIdile(int id)
        {
            using var context = new TodoContext();
            return context.Calismalar.Find(id);
        }

        public void Guncelle(Calisma tablo)
        {
            using var context = new TodoContext();

            //biz bir entity nesne örneğini add update veya delete metotlarıyla gönderdiğimizde arka tarafta örnek olarak güncelleme
            //işlemi yapılıyorsa stateini modified olarak işaretler
            context.Calismalar.Update(tablo);
            context.SaveChanges();
        }

        public void Kaydet(Calisma tablo)
        {
            using var context = new TodoContext();
            context.Calismalar.Add(tablo);
            context.SaveChanges();
        }

        public void Sil(Calisma tablo)
        {
            using var context = new TodoContext();
            context.Calismalar.Remove(tablo);
            context.SaveChanges();
            
        }
    }
}
