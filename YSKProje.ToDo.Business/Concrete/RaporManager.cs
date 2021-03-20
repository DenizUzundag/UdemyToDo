using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Concrete
{
    public class RaporManager : IRaporService
    {
        private readonly EfRaporRepository raporRepository;
        public RaporManager()
        {
            raporRepository = new EfRaporRepository();
        }
        public List<Rapor> GetirHepsi()
        {
            return raporRepository.GetirHepsi();
        }

        public Rapor GetirIdile(int id)
        {
            return raporRepository.GetirIdile(id);
        }

        public void Guncelle(Rapor tablo)
        {
            raporRepository.Guncelle(tablo);
        }

        public void Kaydet(Rapor tablo)
        {
            raporRepository.Kaydet(tablo);
        }

        public void Sil(Rapor tablo)
        {
            raporRepository.Sil(tablo);
        }
    }
}
