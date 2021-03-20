using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Context
{
    public class TodoContext : IdentityDbContext<AppUser,AppRole,int>//primarykey int
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ASUS\\SQLEXPRESS;Database=UdemyBlogToDo;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
        // ilgili classlarımızı tablolarımızı belirtmiş olduk
     
        public DbSet<Gorev> Gorevler { get; set; }
        public DbSet<Aciliyet> Aciliyetler { get; set; }
        public DbSet<Rapor> Raporlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.ApplyConfiguration(new GorevMap());
            modelBuilder.ApplyConfiguration(new AciliyetMap()); 
            modelBuilder.ApplyConfiguration(new RaporMap()); 
            modelBuilder.ApplyConfiguration(new AppUserMap());

            base.OnModelCreating(modelBuilder);

        }
    }
}
