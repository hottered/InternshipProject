using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Register
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Please insert your Email")]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Please insert a valid Email address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please insert strong Password")]
        [Compare("ConfirmPassword", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Please confirm your Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
