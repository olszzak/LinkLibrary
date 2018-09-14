using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLibrary.Entities
{
    public class User 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<Link> Links { get; set; } = new List<Link>();
    }
}
