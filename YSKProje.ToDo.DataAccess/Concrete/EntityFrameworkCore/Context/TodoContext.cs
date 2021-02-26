using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Context
{
    public class TodoContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ASUS\\SQLEXPRESS;Database=UdemyBlogToDo;Trusted_Connection=True;");

        }
        // ilgili classlarımızı tablolarımızı belirtmiş olduk
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Calisma> Calismalar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new KullaniciMap());
            modelBuilder.ApplyConfiguration(new CalismaMap());

        }
    }
}
