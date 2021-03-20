using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Concrete
{
    public class AciliyetManager : IAciliyetService
    {
        private readonly EfAciliyetRepository efAciliyetRepository;
        public AciliyetManager()
        {
            efAciliyetRepository = new EfAciliyetRepository();
        }
        public List<Aciliyet> GetirHepsi()
        {
            return efAciliyetRepository.GetirHepsi();
        }

        public Aciliyet GetirIdile(int id)
        {
            return efAciliyetRepository.GetirIdile(id);
        }

        public void Guncelle(Aciliyet tablo)
        {
            efAciliyetRepository.Guncelle(tablo);
        }

        public void Kaydet(Aciliyet tablo)
        {
            efAciliyetRepository.Kaydet(tablo);
        }

        public void Sil(Aciliyet tablo)
        {
            efAciliyetRepository.Sil(tablo);
        }
    }
}
