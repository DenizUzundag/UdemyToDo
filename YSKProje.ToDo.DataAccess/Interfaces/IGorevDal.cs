using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
    public interface IGorevDal:IGenericDal<Gorev>
    {

        //goreve özgü

        List<Gorev> GetirAciliyetIleTamamlanmayan();
        List<Gorev> GetirTumTablolarla();
        List<Gorev> GetirTumTablolarla(Expression<Func<Gorev,bool>>filter);
        Gorev GetirAciliyetileId(int id);
        List<Gorev> GetirileAppUserId(int appUserId);
        Gorev GetirRaporlarileId(int id);

        List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId, int aktifSayfa);

        int GetirGorevSayisiTamamlananileAppUserId(int id);




    }
}
