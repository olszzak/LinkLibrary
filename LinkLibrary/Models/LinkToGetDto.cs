using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace LinkLibrary.Models
{
    public class LinkToGetDto
    {
        [Required]
        public string Address { get; set; }


        public LinkDetailsDto Details { get; set; }
    }
}
