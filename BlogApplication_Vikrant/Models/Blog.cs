using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApplication_Vikrant.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        public Guid PostedBy { get; set; }
        public string Status { get; set; } = "Enable";
        public string PostedName { get; set; }
        public virtual User Users { get; set; }

    }
}