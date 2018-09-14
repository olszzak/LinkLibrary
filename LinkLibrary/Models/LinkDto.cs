using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLibrary.Models
{
    public class LinkDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public LinkDetailsDto Details { get; set; }
    }
}
