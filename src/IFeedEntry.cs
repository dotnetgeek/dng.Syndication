using System;

namespace dng.Syndication
{
    public interface IFeedEntry
    {
        FeedContent Title { get; set; }

        Uri Link { get; set; }

        FeedContent Summary { get; set; }

        FeedContent Content { get; set; }

        Author Author { get; set; }

        DateTime PublishDate { get; set; }

        DateTime Updated { get; set; }

        Enclosure Enclosure { get; set; }
    }
}