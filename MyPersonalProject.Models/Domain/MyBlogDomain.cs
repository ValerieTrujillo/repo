using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalProject.Models.Domain
{
    public class MyBlogDomain
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string BlogPost { get; set; }
        public string Image { get; set; }
    }
}
