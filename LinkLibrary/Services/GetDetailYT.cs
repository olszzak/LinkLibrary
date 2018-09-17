using LinkLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Serialization;

namespace LinkLibrary.Services
{
    public class GetDetailYT : IGetDetails
    {
        XmlDocument doc;
        string url = "";
        public GetDetailYT(string url)
        {
            this.url = url;
            string videoId;
            if (url.Contains("="))
            {
                // Kuba: Tak samo jak w Vimeo
                int index = url.IndexOf("=");
                videoId = url.Substring(index+1, url.Length - index - 1);
            }else
            {
                int index = url.LastIndexOf("/");
                videoId = url.Remove(index);
            }
            string uri = "https://www.googleapis.com/youtube/v3/videos?id=" + videoId + "&key=AIzaSyCb2IZlmTPeB7amCftKSTPSUHC-wE-kYcU&part=snippet,contentDetails,statistics";

            var request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.MediaType = "GET";
            request.ContentType = "text/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            var kon = sr.ReadToEnd();

            doc = (XmlDocument)JsonConvert.DeserializeXmlNode(kon, "root");
        }
       

        public string GetDuration()
        {
            XmlNodeList nodelist = doc.SelectNodes("root/items/contentDetails").Item(0).ChildNodes;
            return nodelist.Item(0).InnerText;
        }

        public int GetLikes()
        {
            XmlNodeList nodelist = doc.SelectNodes("root/items/statistics").Item(0).ChildNodes;
            return Convert.ToInt32(nodelist.Item(1).InnerText);
        }

        public string GetProvider()
        {
            return "YouTube";
        }

        public string GetThumbnailUrl()
        {
            XmlNodeList nodelist = doc.SelectNodes("root/items/snippet/thumbnails/default").Item(0).ChildNodes;
            return nodelist.Item(0).InnerText;
        }

        public string GetTitle()
        {
            XmlNodeList nodelist = doc.SelectNodes("root/items/snippet").Item(0).ChildNodes;
            return nodelist.Item(2).InnerText;
        }

        public string GetUserName()
        {
            XmlNodeList nodelist = doc.SelectNodes("root/items/snippet").Item(0).ChildNodes;
            return nodelist.Item(5).InnerText;
        }
    }
}
