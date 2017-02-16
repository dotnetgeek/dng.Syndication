using System;
using dng.Syndication.Attributes;

namespace dng.Syndication
{
    public interface IFeedEntry
    {
        /// <summary>
        /// Title of the item 
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// The hyperlink to the item 
        /// </summary>
        Uri Link { get; set; }

        string Summary { get; set; }

        /// <summary>
        /// Content the item
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// Author is used to specify the e-mail address of the author of an item
        /// </summary>
        Author Author { get; set; }

        /// <summary>
        /// Last publication date for the item
        /// </summary>
        DateTime PublishDate { get; set; }

        DateTime Updated { get; set; }
    }
}