using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Context;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfBildirimRepository : EfGenericRepository<Bildirim>, IBildirimDal
    {
        public int GetirOkunmayanBildirimSayisiAppUserId(int AppUserId)
        {
            using var context = new TodoContext();
            return context.Bildirimler.Count(I => I.AppUseId == AppUserId && !I.Durum);
        }

        public List<Bildirim> GetirOkunmayanlar(int AppUserId)
        {
            //okunmaması için durumun false olması lazım
            using var context = new TodoContext();
            return context.Bildirimler.Where(I => I.AppUseId == AppUserId && !I.Durum).ToList();
        }
    }
}
