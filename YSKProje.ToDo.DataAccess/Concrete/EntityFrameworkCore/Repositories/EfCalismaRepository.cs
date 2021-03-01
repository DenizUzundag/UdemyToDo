using System;
using System.Collections.Generic;
using System.Linq;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Context;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCalismaRepository : EfGenericRepository<Calisma>, ICalismaDal
    {
       //ICalismaDalı yazmamızın sebebi calima ile ilgili özel bir mett varsa gelsin diye
    }
}
