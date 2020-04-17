using System;
using System.Collections.Generic;
using System.Text;

namespace dng.Syndication.Models.Podcasts
{
    public class Link
    {
        public string Href { get; set; } = @"self";

        public string Rel { get; set; } = @"self";

        public string Type { get; set; } = @"application/rss+xml";
    }
}
