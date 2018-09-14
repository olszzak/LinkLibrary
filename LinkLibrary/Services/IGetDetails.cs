using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLibrary.Services
{
    public interface IGetDetails
    {
        string GetTitle();
        string GetProvider();
        string GetThumbnailUrl();
        string GetUserName();
        string GetDuration();
        int GetLikes();
    }
}
