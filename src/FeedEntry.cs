using System;
using dng.Syndication.Attributes;

namespace dng.Syndication
{

    public class FeedEntry : IFeedEntry
    {
        /// <summary>
        /// Title of the item 
        /// </summary>
        [Rss20Property("title")]
        [AtomProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// The hyperlink to the item 
        /// </summary>
        [Rss20Property("guid")]
        [Rss20Property("link")]
        [AtomProperty("link", "href")]
        [AtomProperty("id")]
        public Uri Link { get; set; }

        [Rss20Ignore]
        [AtomProperty("summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Content the item
        /// </summary>
        [Rss20Property("description")]
        [AtomProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Author is used to specify the e-mail address of the author of an item
        /// </summary>
        [Rss20Property("author")]
        [AtomProperty("author")]
        public Author Author { get; set; }

        /// <summary>
        /// Last publication date for the item
        /// </summary>
        [Rss20Property("pubDate")]
        [AtomProperty("published")]
        public DateTime PublishDate { get; set; }

        [Rss20Ignore]
        [AtomProperty("updated")]
        public DateTime Updated { get; set; }
    }
}
