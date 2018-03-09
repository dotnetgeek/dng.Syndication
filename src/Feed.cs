using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using dng.Syndication.Attributes;
using dng.Syndication.Writers;

namespace dng.Syndication
{
    public class Feed : IFeed
    {
        /// <summary>
        /// Name for the feed.
        /// </summary>
        [AtomProperty("title", Required = true, Writer = typeof(ContentElementWriter))]
        [Rss20Property("title", Required = true, Writer = typeof(ContentElementWriter))]
        public FeedContent Title { get; set; }

        /// <summary>
        /// Author is used to specify the e-mail address of the author of an item
        /// </summary>
        [AtomProperty("author", Required = true, Writer = typeof(AtomAuthorElementWriter))]
        [Rss20Ignore]
        public Author Author { get; set; }

        /// <summary>
        /// Defines the hyperlink to the channel
        /// </summary>
        [AtomProperty("id", Required = true)]
        [AtomProperty("link", NamespaceUri = Namespaces.AtomNamespace, Writer = typeof(AtomLinkElementWriter))]
        [Rss20Property("link", NamespaceUri = Namespaces.AtomNamespace, Writer = typeof(AtomLinkElementWriter))]
        [Rss20Property("link", Required = true, Writer = typeof(Rcf3986UriElementFormatter))]
        public Uri Link { get; set; }

        /// <summary>
        /// Defines the last-modified date of the content of the feed
        /// </summary>
        [AtomProperty("updated", Required = true, Writer = typeof(AtomDateTimeElementWriter))]
        [Rss20Property("lastBuildDate", Writer = typeof(Rss20DateTimeWriter))]
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Specifies the language the feed is written in
        /// </summary>
        [AtomIgnore]
        [Rss20Property("language", Writer = typeof(Rss20LanguageElementWriter))]
        public CultureInfo Language { get; set; }

        /// <summary>
        /// The copyright child element notifies about copyrighted material.
        /// </summary>
        [AtomProperty("rights")]
        [Rss20Property("copyright")]
        public string Copyright { get; set; }

        /// <summary>
        /// Defines the publication date for the content of the feed
        /// </summary>
        [AtomProperty("published", Writer = typeof(AtomDateTimeElementWriter))]
        [Rss20Property("pubDate", Writer = typeof(Rss20DateTimeWriter))]
        public DateTime PublishedDate { get; set; }

        /// <summary>
        /// Specifies the program used to generate the feed
        /// </summary>
        [AtomProperty("generator")]
        [Rss20Property("generator")]
        public string Generator { get; set; } =
            $"{Assembly.GetExecutingAssembly().GetName().Name}/{Assembly.GetExecutingAssembly().GetName().Version.ToString(2)}";

        /// <summary>
        /// Describes the feed
        /// </summary>
        [AtomProperty("subtitle", Writer = typeof(ContentElementWriter))]
        [Rss20Property("description", Required = true, Writer = typeof(ContentElementWriter))]
        public FeedContent Description { get; set; }

        /// <summary>
        /// Specifies a GIF, JPEG or PNG image that can be displayed with the channel
        /// </summary>
        [AtomProperty("logo", Writer = typeof(AtomImageUrlElementWriter))]
        [Rss20Property("image", Writer = typeof(Rss20ImageElementWriter))]
        public Image Image { get; set; }

        /// <summary>
        /// Specifies an icon that can be displayed with the channel (fav.ico?)
        /// </summary>
        [AtomProperty("icon", Writer = typeof(AtomImageUrlElementWriter))]
        [Rss20Ignore]
        public Image Icon { get; set; }

        /// <summary>
        /// Defines the e-mail address to the webmaster of the feed
        /// </summary>
        [AtomIgnore]
        [Rss20Property("webMaster", Writer = typeof(Rss20AuthorElementWriter))]
        public Author WebMaster {get; set; }

        /// <summary>
        /// Defines the e-mail address to the webmaster of the feed
        /// </summary>
        [AtomIgnore]
        [Rss20Property("managingEditor", Writer = typeof(Rss20AuthorElementWriter))]
        public Author ManagingEditor { get; set; }

        [AtomIgnore]
        [Rss20Property("ttl")]
        public int? TimeToLive { get; set; }

        /// <summary>
        /// Each FeedEntries element defines an article or "story" in an RSS feed.
        /// </summary>
        [AtomProperty("entry")]
        [Rss20Property("item")]
        public IList<IFeedEntry> FeedEntries { get; set; }
    }
}