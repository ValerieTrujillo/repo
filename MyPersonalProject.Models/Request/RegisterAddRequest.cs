using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalProject.Models.Request
{
    public class RegisterAddRequest
    {
        [Required]
        [MaxLength(255)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(1)]
        [Display(Name = "Middle Initial")]
        public string MiddleInitial { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Salt { get; set; }
        [Required]
        public string Password { get; set; }

        public string ModifiedBy { get; set; }
    }
}
