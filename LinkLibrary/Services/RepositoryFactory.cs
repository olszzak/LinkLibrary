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
            // TODO BP: ten sposób wyciągania serwisu z URLa jest mocno naiwny.
            //     np. url https://vimeo.com/1234?youtube=true mimo że jest URLem Vimeo, zostanie zaliczony jako URL YT.
            if (url.Contains("youtube") || url.Contains("youtu")) return details = new GetDetailYT(url);

            if (url.Contains("vimeo")) return details = new GetDetailVimeo(url);

            return details;
        }
    }
}
