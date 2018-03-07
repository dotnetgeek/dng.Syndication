using System;
using dng.Syndication.Attributes;
using dng.Syndication.Enums;
using dng.Syndication.Writers;

namespace dng.Syndication
{
    public class FeedEntry : IFeedEntry
    {
        /// <summary>
        /// Title of the item 
        /// </summary>
        [AtomProperty("title", Writer = typeof(ContentElementWriter))]
        [Rss20Property("title", Writer = typeof(ContentElementWriter))]
        public FeedContent Title { get; set; }

        /// <summary>
        /// The hyperlink to the item 
        /// </summary>
        [AtomProperty("link", "href", Writer = typeof(Rcf3986UriElementFormatter))]
        [AtomProperty("id", Writer = typeof(Rcf3986UriElementFormatter))]
        [Rss20Property("guid", Writer = typeof(Rcf3986UriElementFormatter))]
        [Rss20Property("link", Writer = typeof(Rcf3986UriElementFormatter))]
        public Uri Link { get; set; }

        [AtomProperty("summary", Writer = typeof(ContentElementWriter))]
        [Rss20Ignore]
        public FeedContent Summary { get; set; }

        /// <summary>
        /// Content the item
        /// </summary>
        [AtomProperty("content", Writer = typeof(ContentElementWriter))]
        [Rss20Property("description", Writer = typeof(ContentElementWriter))]
        public FeedContent Content { get; set; }

        /// <summary>
        /// Author is used to specify the e-mail address of the author of an item
        /// </summary>
        [InheritProperty(nameof(Feed.Author), FeedTypes = new [] {FeedType.Atom} )]
        [AtomProperty("author", Writer = typeof(AtomAuthorElementWriter))]
        [Rss20Property("author", Writer = typeof(Rss20AuthorElementWriter))]
        public Author Author { get; set; }

        /// <summary>
        /// Last publication date for the item
        /// </summary>
        [AtomProperty("published", Writer = typeof(AtomDateTimeElementWriter))]
        [Rss20Property("pubDate", Writer = typeof(Rss20DateTimeWriter))]
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Last updated date for the item
        /// </summary>
        [AtomProperty("updated", Writer = typeof(AtomDateTimeElementWriter))]
        [Rss20Ignore]
        public DateTime Updated { get; set; }

        /// <summary>
        /// http://www.rssboard.org/rss-specification#ltenclosuregtSubelementOfLtitemgt
        /// </summary>
        [AtomIgnore]
        [Rss20Property("enclosure", Writer = typeof(Rss20EnclosureElementWriter))]
        public Enclosure Enclosure { get; set; }
    }
}
