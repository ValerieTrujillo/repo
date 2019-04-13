using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalProject.Models.Request
{
    public class MyBlogUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Header { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string BlogPost { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
