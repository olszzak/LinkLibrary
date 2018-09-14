using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLibrary.Models
{
    public class LinkToAdd
    {
        [Required(ErrorMessage ="Please enter link")]
        [Display(Name ="Address")]
        public string Address { get; set; }
    }
}
