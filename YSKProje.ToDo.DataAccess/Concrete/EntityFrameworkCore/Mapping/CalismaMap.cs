using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class CalismaMap : IEntityTypeConfiguration<Calisma>
    {
        public void Configure(EntityTypeBuilder<Calisma> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Ad).HasMaxLength(200);
            builder.Property(x => x.Aciklama).HasColumnType("ntext");


        }
    }
}
