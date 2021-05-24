using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Interfaces
{
    public interface IAppUserService
    {
        List<AppUser> GetirAdminOlmayanlar();
        List<AppUser> GetirAdminOlmayanlar(out int toplamSayfa, string aranacakKElime, int aktifSayfa = 1);
        List<DualHelper> GetirEnCokGorevTamamlamisPersoneller();
        List<DualHelper> GetirEnCokGorevdeCalisanPersoneller();
    }
        
}
