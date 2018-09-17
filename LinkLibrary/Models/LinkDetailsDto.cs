using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLibrary.Models
{
    public class LinkDetailsDto
    {
        /*
        public string html { get; set; }
        public string title { get; set; }
        public int thumbnail_height { get; set; }
        public string provider_name { get; set; }
        public string author_url { get; set; }
        public int thumbnail_width { get; set; }
        public int height { get; set; }
        public string provider_url { get; set; }
        public string type { get; set; }
        public int width { get; set; }
        public string version { get; set; }
        public string author_name { get; set; }
        public string thumbnail_url { get; set; }
        */
        public string Title { get; set; }
        public string Provider { get; set; }
        public string ThumbnailUrl { get; set; }
        public string AuthorName { get; set; }
        public string Duration { get; set; }
        public int ViewCount { get; set; }
        public int Likes { get; set; }

       // public Video video { get; set; }
    }

}
