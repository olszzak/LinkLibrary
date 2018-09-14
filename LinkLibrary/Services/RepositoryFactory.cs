using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLibrary.Services
{
    public static class RepositoryFactory
    {
        public static IGetDetails GetHosting(string url)
        {
            IGetDetails details = null;
            if (url.Contains("youtube") || url.Contains("youtu")) return details = new GetDetailYT(url);

            if (url.Contains("vimeo")) return details = new GetDetailVimeo(url);

            return details;
        }
    }
}
