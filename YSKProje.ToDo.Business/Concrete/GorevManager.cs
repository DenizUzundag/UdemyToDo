using System.Collections.Generic;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Concrete
{
    public class GorevManager:IGorevService
    {
        private readonly EfGorevRepository efCalismaRepository;
        public GorevManager()
        {
            efCalismaRepository = new EfGorevRepository();
        }
        public List<Gorev> GetirHepsi()
        {
            return efCalismaRepository.GetirHepsi();
        }

        public Gorev GetirIdile(int id)
        {
            return efCalismaRepository.GetirIdile(id);

        }

        public void Guncelle(Gorev tablo)
        {
            efCalismaRepository.Guncelle(tablo);
        }

        public void Kaydet(Gorev tablo)
        {
            efCalismaRepository.Kaydet(tablo);
        }

        public void Sil(Gorev tablo)
        {
            efCalismaRepository.Sil(tablo);
        }
    }
}
