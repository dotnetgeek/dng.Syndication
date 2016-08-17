using System;

namespace dng.Syndication
{

    public class FeedEntry
    {
        public string Title { get; set; }

        public Uri Link { get; set; }

        public string Id { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public Author Author { get; set; }
        public DateTime PublishDate { get; set; }

        public DateTime Updated{ get; set; }
    }
}
