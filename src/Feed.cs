using System;
using System.Collections.Generic;

namespace dng.Syndication
{
    public class Feed
    {
        /// <summary>
        /// Name for the feed.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Author is used to specify the e-mail address of the author of an item
        /// </summary>
        public Author Author { get; set; }

        /// <summary>
        /// Defines the hyperlink to the channel
        /// </summary>
        public Uri Link { get; set; }

        /// <summary>
        /// Defines the last-modified date of the content of the feed
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Each FeedEntries element defines an article or "story" in an RSS feed.
        /// </summary>
        public IList<IFeedEntry> FeedEntries { get; set; }

        /// <summary>
        /// Specifies the language the feed is written in
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// The copyright child element notifies about copyrighted material.
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// Defines the publication date for the content of the feed
        /// </summary>
        public DateTime PublishedDate { get; set; }

        /// <summary>
        /// Specifies the program used to generate the feed
        /// </summary>
        public string Generator { get; set; }

        /// <summary>
        /// Describes the feed
        /// </summary>
        public string Description { get; set; }
    }
}