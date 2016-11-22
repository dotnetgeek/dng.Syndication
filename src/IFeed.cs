using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dng.Syndication.Attributes;

namespace dng.Syndication
{
    internal interface IFeed
    {
        /// <summary>
        /// Name for the feed.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Author is used to specify the e-mail address of the author of an item
        /// </summary>
        Author Author { get; set; }

        /// <summary>
        /// Defines the hyperlink to the channel
        /// </summary>
        Uri Link { get; set; }

        /// <summary>
        /// Defines the last-modified date of the content of the feed
        /// </summary>
        DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Each FeedEntries element defines an article or "story" in an RSS feed.
        /// </summary>
        IList<IFeedEntry> FeedEntries { get; set; }

        /// <summary>
        /// Specifies the language the feed is written in
        /// </summary>
        string Language { get; set; }

        /// <summary>
        /// The copyright child element notifies about copyrighted material.
        /// </summary>
        string Copyright { get; set; }

        /// <summary>
        /// Defines the publication date for the content of the feed
        /// </summary>
        DateTime PublishedDate { get; set; }

        /// <summary>
        /// Specifies the program used to generate the feed
        /// </summary>
        string Generator { get; set; }

        /// <summary>
        /// Describes the feed
        /// </summary>
        string Description { get; set; }

        Image Logo { get; set; }
        
        Image Icon { get; set; }
    }
}
