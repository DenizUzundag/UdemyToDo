using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Models
{
    public class AppUserSignInModel
    {
        [Required(ErrorMessage ="Kullanıcı adı boş geçilemez")]
        [Display(Name ="Kullanıcı Adı:")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Parola:")]
        [Required(ErrorMessage ="Parola alanı boş geçilemez")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }

 
}
