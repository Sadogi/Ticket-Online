using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketOnlineWeb.Models.Forms
{
    public class EditUser
    {
        [MaxLength(75)]
        public string LastName { get; set; }
        [MaxLength(75)]
        public string FirstName { get; set; }
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
        [MaxLength(100)]
        public string Address { get; set; }
    }
}
