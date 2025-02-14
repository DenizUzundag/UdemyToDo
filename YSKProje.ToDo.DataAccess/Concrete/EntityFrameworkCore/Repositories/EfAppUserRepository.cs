﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Context;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfAppUserRepository :  IAppUserDal
    {
        public List<AppUser> GetirAdminOlmayanlar()
        {
            /*select * form AspNetUsers inner join AspNetUserRoles
             on AspNetUsers.Id=AspNetUserRoles.UserId 
            inner join AspNetRoles
            on AspNetUserRoles.RoleId = AspNetRole.Id where AspNetRoles.Name= 'Member'*/
            using var context = new TodoContext();
            return context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            {

                user = resultUser,
                userRole = resultUserRole


            }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id, (resultTable, resultRole) => new
            {
                user = resultTable.user,
                userRole =resultTable.userRole,
                roles=resultRole

            }).Where(I=>I.roles.Name=="Member").Select(I=>new AppUser()
            { 
               Id=I.user.Id,
                Name = I.user.Name,
                Surname = I.user.Surname,
                Picture =I.user.Picture,
               Email=I.user.Email,
               UserName =I.user.UserName
            }).ToList();
                
               
                
                /*.Where(I => I.userRole.RoleId == 5).Select(I => new AppUser
            {
                Id=I.user.Id,
            });*/


        }
        public List<AppUser> GetirAdminOlmayanlar(out int toplamSayfa, string aranacakKElime, int aktifSayfa = 1)
        {

            using var context = new TodoContext();
            var result = context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            {

                user = resultUser,
                userRole = resultUserRole


            }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id, (resultTable, resultRole) => new
            {
                user = resultTable.user,
                userRole = resultTable.userRole,
                roles = resultRole

            }).Where(I => I.roles.Name == "Member").Select(I => new AppUser()
            {
                Id = I.user.Id,
                Name = I.user.Name,
                Surname=I.user.Surname,
                Picture = I.user.Picture,
                Email = I.user.Email,
                UserName = I.user.UserName
            });

            toplamSayfa = (int)Math.Ceiling((double)result.Count()/3);
            if (!string.IsNullOrWhiteSpace(aranacakKElime))//boşsa
            {
               result = result.Where(I => I.Name.ToLower().Contains(aranacakKElime.ToLower()) || I.Surname.ToLower().Contains(aranacakKElime.ToLower()));
                toplamSayfa = (int)Math.Ceiling((double)result.Count() / 3);
            }

           result = result.Skip((aktifSayfa - 1) * 3).Take(3);
            return result.ToList();





        }
        public List<DualHelper> GetirEnCokGorevTamamlamisPersoneller()
        {
            using var context = new TodoContext();
            return context.Gorevler.Include(I => I.AppUser).Where(I => I.Durum).GroupBy(I => I.AppUser.UserName)
                 .OrderByDescending(I => I.Count()).Take(5).Select(I => new DualHelper {
                     Isim = I.Key,
                     GorevSayisi = I.Count()
                 }).ToList();
            
        }
        public List<DualHelper> GetirEnCokGorevdeCalisanPersoneller()
        {
            using var context = new TodoContext();
            return context.Gorevler.Include(I => I.AppUser).Where(I => !I.Durum && I.AppUserId !=null).GroupBy(I => I.AppUser.UserName)
                 .OrderByDescending(I => I.Count()).Take(5).Select(I => new DualHelper
                 {
                     Isim = I.Key,
                     GorevSayisi = I.Count()
                 }).ToList();

        }
    }

    //class ThreeModel
    //{
    //    public AppUser AppUser { get; set; }
    //    public AppRole AppRole { get; set; }
    //}
   
}
