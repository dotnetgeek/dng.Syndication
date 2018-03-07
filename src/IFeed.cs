using System;
using System.Collections.Generic;
using System.Globalization;

namespace dng.Syndication
{
    public interface IFeed
    {
        FeedContent Title { get; set; }

        Author Author { get; set; }

        Uri Link { get; set; }

        DateTime UpdatedDate { get; set; }

        IList<IFeedEntry> FeedEntries { get; set; }

        CultureInfo Language { get; set; }

        string Copyright { get; set; }

        DateTime PublishedDate { get; set; }

        string Generator { get; set; }

        FeedContent Description { get; set; }

        Image Image { get; set; }

        Image Icon { get; set; }

        Author WebMaster { get; set; }

        Author ManagingEditor { get; set; }

        int? TimeToLive { get; set; }
    }
}