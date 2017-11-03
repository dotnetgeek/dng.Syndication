using System;

namespace dng.Syndication
{
    public class FeedEntry
    {
        /// <summary>
        /// Title of the item 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The hyperlink to the item 
        /// </summary>
        public Uri Link { get; set; }

        public string Summary { get; set; }

        /// <summary>
        /// Content the item
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Author is used to specify the e-mail address of the author of an item
        /// </summary>
        public Author Author { get; set; }

        /// <summary>
        /// Last publication date for the item
        /// </summary>
        public DateTime PublishDate { get; set; }

        public DateTime Updated { get; set; }

        //http://www.rssboard.org/rss-specification#ltenclosuregtSubelementOfLtitemgt
        public Enclosure Enclosure { get; set; }
    }
}
