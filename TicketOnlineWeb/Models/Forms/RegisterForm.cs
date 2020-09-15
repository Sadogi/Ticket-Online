using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketOnlineWeb.Models.Forms
{
    public class RegisterForm
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [DisplayName("Screen Name")]
        public string ScreenName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(384)]
        [DisplayName("Email Address")]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-=]).{8,20}$")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Passwd { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-=]).{8,20}$")]
        [DataType(DataType.Password)]
        [Compare(nameof(Passwd))]
        [Display(Name = "Confirm Password")]
        public string Confirm { get; set; }
    }
}
