using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Piccolo.Models;

namespace Piccolo.ViewModels
{
    public class UrlsViewModel
    {
        public List<PiccoloShortUrl> NewUrls { get; set; }
        public List<PiccoloShortUrl> PopularUrls { get; set; }
        public List<PiccoloShortUrl> RecentUrls { get; set; }
    }

    public class PiccoloShortUrl
    {
        public string ShortUrl { get; set; }

        public string DateFormatted { get; set; }

        public int CountFormatted { get; set; }

        public static PiccoloShortUrl From(string absoluteUrl, string route, string dateFormatted, int countFormatted)
        {
            return new PiccoloShortUrl
            {
                ShortUrl = new Uri(new Uri(absoluteUrl), route).ToString(),
                DateFormatted = dateFormatted,
                CountFormatted = countFormatted
            };
        }
    }

}