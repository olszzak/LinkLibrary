using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLibrary.Services
{
    // TODO BP: GetDetails czego?
    // TODO BP: interfejsów nie nazywamy od wykonywanej funkcji. powinno być coś w stylu IVideoDetails.
    // TODO BP: generalnie to nie jestem przekonany co do takiego rozwiązania.
    //     powinno to być podzielone na 2 interfejsy, albo interfejs i klasę: IVideoService i VideoDetails.
    //     w IVideoService masz metodę GetVideoDetails biorącą URL i zwracającą obiekt typu VideoDetails.
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
