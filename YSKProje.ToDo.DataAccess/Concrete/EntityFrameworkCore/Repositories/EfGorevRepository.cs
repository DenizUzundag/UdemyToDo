using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Context;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGorevRepository : EfGenericRepository<Gorev>, IGorevDal
    {
        public Gorev GetirAciliyetileId(int id)
        {
            using var contex = new TodoContext();
            return contex.Gorevler.Include(I => I.Aciliyet).FirstOrDefault(I => !I.Durum && I.Id == id); //durumu false olanlar gelsin olusturlma tarihine göre
        }

        public Gorev GetirRaporlarileId(int id)
        {
            using var context = new TodoContext();
            return context.Gorevler.Include(I => I.Raporlar).Include(I=>I.AppUser).Where(I => I.Id == id).FirstOrDefault();

        }

        //ICalismaDalı yazmamızın sebebi calima ile ilgili özel bir mett varsa gelsin diye
        public List<Gorev> GetirAciliyetIleTamamlanmayan()
        {
           using (var contex = new TodoContext())
            {
              return contex.Gorevler.Include(I => I.Aciliyet).Where(I => !I.Durum).OrderByDescending(I=>I.OlusturulmaTarihi).ToList(); //durumu false olanlar gelsin olusturlma tarihine göre

            }
        }

        public List<Gorev> GetirileAppUserId(int appUserId)
        {
            using var context = new TodoContext();
            return context.Gorevler.Where(I => I.AppUserId == appUserId).ToList();
        }

        public List<Gorev> GetirTumTablolarla()
        {
            using (var contex = new TodoContext())
            {
                return contex.Gorevler.Include(I => I.Aciliyet).Include(I=>I.Raporlar).Include(I=>I.AppUser)
                    .Where(I => !I.Durum).OrderByDescending(I => I.OlusturulmaTarihi).ToList(); //durumu false olanlar gelsin olusturlma tarihine göre

            }
        }

        public List<Gorev> GetirTumTablolarla(Expression<Func<Gorev, bool>> filter)
        {
            using (var contex = new TodoContext())
            {
                return contex.Gorevler.Include(I => I.Aciliyet).Include(I => I.Raporlar).Include(I => I.AppUser)
                    .Where(filter).OrderByDescending(I => I.OlusturulmaTarihi).ToList(); //durumu false olanlar gelsin olusturlma tarihine göre

            }
        }

        public List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId ,int aktifSayfa=1)
        {
            using var contex = new TodoContext();

            var returnValue = contex.Gorevler.Include(I => I.Aciliyet).Include(I => I.Raporlar).Include(I => I.AppUser)
                  .Where(I => I.AppUserId == userId && I.Durum==true).OrderByDescending(I => I.OlusturulmaTarihi);//durumu false olanlar gelsin olusturlma tarihine göre

            toplamSayfa = (int)Math.Ceiling((double)returnValue.Count() / 3);

            return returnValue.Skip((aktifSayfa - 1) * 3).Take(3).ToList();

        }

        public int GetirGorevSayisiTamamlananileAppUserId(int id)
        {
            using var context = new TodoContext();
            return context.Gorevler.Count(I => I.AppUserId == id && !I.Durum);
        }
    }
}
