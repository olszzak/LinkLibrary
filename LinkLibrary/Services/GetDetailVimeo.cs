using AngleSharp;
using AngleSharp.Dom.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Runtime.Serialization.Json;
using LinkLibrary.Models;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace LinkLibrary.Services
{
    // TODO BP: tak się nazywa metody nie klasy. jak już to VimeoVideoDetails. patrz komentarz w IGetDetails
    // TODO BP: brak obsługi wyjątków
    public class GetDetailVimeo : IGetDetails
    {
        XElement xelement;
        string url = "";
        public GetDetailVimeo(string url)
        {
            this.url = url;
            string videoId;
            int index = url.LastIndexOf("/");
            videoId = url.Substring(index + 1, url.Length - index - 1);

            // TODO BP: url do consta
            string uri = "http://vimeo.com/api/v2/video/" + videoId + ".xml";

            // TODO BP: mieszasz 2 konwencje, raz dajesz var raz dajesz typ
            var request = (HttpWebRequest)HttpWebRequest.Create(uri);
            // TODO BP: System.Net.WebRequestMethods.Http.Get, albo przynajmniej zrób swoją klase z tymi constami
            // TODO BP: pomyliłeś MediaType z Method. domyślną wartością method już jest "GET"
            request.MediaType = "GET";
            // TODO BP: jw., System.Net.Mime.MediaTypeNames.Text.Xml
            request.ContentType = "text/xml";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            xelement = XElement.Load(response.GetResponseStream());
        }

        public string GetThumbnailUrl()
        {
            XElement thumbnails = xelement.Elements().Elements("thumbnail_small").Single();
            return thumbnails.Value;
        }

        public string GetTitle()
        {
            XElement title = xelement.Elements().Elements("title").Single();
            return title.Value;
        }

        public string GetProvider()
        {
            // TODO BP: do consta
            return "Vimeo";
        }

        public string GetUserName()
        {
            XElement user = xelement.Elements().Elements("user_name").Single();
            return user.Value;
        }

        public string GetDuration()
        {
            XElement durationVim = xelement.Elements().Elements("duration").Single();
            return durationVim.Value;
        }

        public int GetLikes()
        {
            XElement likes = xelement.Elements().Elements("stats_number_of_likes").Single();
            return Convert.ToInt32(likes.Value);
        }
    }
}

