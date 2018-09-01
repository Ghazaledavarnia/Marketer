using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Models
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "وارد کردن نام الزامی می باشد")]
        public string Name { get; set; }
        [Required(ErrorMessage = "وارد کردن تلفن همراه الزامی می باشد")]
        [RegularExpression(RegexPatten.CellPhone, ErrorMessage = "فرمت تلفن همراه صحیح نمی باشد")]
        public string Mobile { get; set; }
        public string Note { get; set; }
        [Required(ErrorMessage = "وارد کردن صنف الزامی می باشد")]
        [Display(Name = "صنف:")]
        public string Class { get; set; }
        public string ClassId { get; set; }
                                           // public string ApplicationUserId { get; set; }
    }
}
