using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalProject.Services
{
    public class LoginService
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    
       public int Insert()
        {
            return 0;
        }
     }
}
