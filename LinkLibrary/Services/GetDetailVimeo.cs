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

            string uri = "http://vimeo.com/api/v2/video/" + videoId + ".xml";

            var request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.MediaType = "GET";
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

