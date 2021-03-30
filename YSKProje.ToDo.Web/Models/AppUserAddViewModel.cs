using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Models
{
    public class AppUserAddViewModel
    {
        [Required(ErrorMessage ="Kullanıcı adı boş geçilemez")]
        [Display(Name ="Kullanıcı adı:")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]

        [Display(Name = "şifre:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       

        [Display(Name = "tekrar şifre:")]
        [DataType(DataType.Password)]

        [Compare("Password", ErrorMessage = "Parolalar eşleşmiyor")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Email:")]
        [EmailAddress(ErrorMessage ="geçersiz email")]
        [Required(ErrorMessage = "Email boş geçilemez")]
        public string Email { get; set; }


        [Display(Name = "Adınız:")]

        [Required(ErrorMessage = "Name boş geçilemez")]
        public string Name { get; set; }

        [Display(Name = "Surname:")]

        [Required(ErrorMessage = "Surname adı boş geçilemez")]
        public string Surname { get; set; }


    }
}
