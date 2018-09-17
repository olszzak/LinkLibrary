using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLibrary.Models
{
    // Kuba: Taki sam model jak LinkToAdd. Możesz używać tamtego, a ten usunąć
    public class LinkToAddDto
    {
        [Required(ErrorMessage ="Please enter link")]
        [Display(Name ="Address")]
        public string Address { get; set; }

    }
}
