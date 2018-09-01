using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "وارد کردن تلفن همراه الزامی می باشد")]
        [RegularExpression(RegexPatten.CellPhone, ErrorMessage = "فرمت تلفن همراه صحیح نمی باشد")]
        public string UserName { get; set; }
      //  [Display(Name = "موبایل")]
     //   [Required(ErrorMessage = "وارد کردن تلفن همراه الزامی می باشد")]
       // [RegularExpression(RegexPatten.CellPhone, ErrorMessage = "فرمت تلفن همراه صحیح نمی باشد")]
     //   public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="رمز عبور را وارد کنید")]
       //[StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
       [StringLength(10, ErrorMessage = "رمز عبور حداقل 4 کاراکتر و حداکثر 10 کاراکترباشد", MinimumLength = 4)]
       [DataType(DataType.Password)]
        [Display(Name = "رمزعبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمزعبور")]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور با رمز اصلی تطابق ندارد")]
        public string ConfirmPassword { get; set; }
    }
}
