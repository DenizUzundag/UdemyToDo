using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //ICalismaDalı yazmamızın sebebi calima ile ilgili özel bir mett varsa gelsin diye
        public List<Gorev> GetirAciliyetIleTamamlanmayan()
        {
           using (var contex = new TodoContext())
            {
              return contex.Gorevler.Include(I => I.Aciliyet).Where(I => !I.Durum).OrderByDescending(I=>I.OlusturulmaTarihi).ToList(); //durumu false olanlar gelsin olusturlma tarihine göre

            }
        }

        public List<Gorev> GetirTumTablolarla()
        {
            using (var contex = new TodoContext())
            {
                return contex.Gorevler.Include(I => I.Aciliyet).Include(I=>I.Raporlar).Include(I=>I.AppUser)
                    .Where(I => !I.Durum).OrderByDescending(I => I.OlusturulmaTarihi).ToList(); //durumu false olanlar gelsin olusturlma tarihine göre

            }
        }
    }
}
