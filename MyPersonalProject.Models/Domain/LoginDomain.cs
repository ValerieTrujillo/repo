using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalProject.Models.Domain
{
    public class LoginDomain
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
