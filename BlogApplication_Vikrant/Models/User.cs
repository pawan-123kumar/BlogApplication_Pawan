using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApplication_Vikrant.Models
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; } = "User";
        

        public ICollection<Blog> Blogs { get; set; }
    }
}