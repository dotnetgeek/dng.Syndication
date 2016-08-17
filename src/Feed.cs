using System;
using System.Collections.Generic;

namespace dng.Syndication
{
    public class Feed
    {
        public string Title { get; set; }

        public Author Author { get; set; }

        public  Uri Link { get; set; }

        public string Id {get; set; }

        public DateTime UpdatedDate { get; set; }

        public IList<FeedEntry> FeedEntries { get; set; }

        public string Language { get; set; }

        public string Copyright { get; set; }

        public DateTime Published { get; set; }

        public string Generator { get; set; }

        public  string Description { get; set; }

        public Uri Self { get; set; }
    }

}
