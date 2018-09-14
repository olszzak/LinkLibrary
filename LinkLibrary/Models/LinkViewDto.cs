using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLibrary.Models
{
    public class LinkViewDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string Provider { get; set; }
        public string ThumbnailUrl { get; set; }
        public string AuthorName { get; set; }
        public string Duration { get; set; }
        public int viewCount { get; set; }
        public int Likes { get; set; }
        public int UserId { get; set; }
    }
}
