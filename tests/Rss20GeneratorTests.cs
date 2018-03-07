using System;
using System.Collections.Generic;
using System.Globalization;
using dng.Syndication.Generators;
using Xunit;

namespace dng.Syndication.Tests
{
    public class Rss20GeneratorTests
    {
        private readonly string _dateTimeOffset = new DateTime(2016, 08, 16).ToString("zzzz").Replace(":", "");

        private Feed CreateSimpleFeed()
        {
            var feed = new Feed
            {
                Title = FeedContent.Plain("dotnetgeek feed"),
                Author = new Author("Daniel", "email@email.em"),
                Copyright = "2016 @ www.dotnetgeek.com",
                Description = FeedContent.Plain("Dotnet relevant topics"),
                Generator = "dng.Syndication",
                Language = CultureInfo.GetCultureInfo("de"),
                UpdatedDate = new DateTime(2016, 08, 16),
                Link = new Uri("http://www.dotnetgeek.de/rss"),
                FeedEntries = new List<IFeedEntry>
                {
                    new FeedEntry
                    {
                        Title =  FeedContent.Plain("First Entry"),
                        Content =  FeedContent.Plain("Content"),
                        Link = new Uri("http://www.dotnetgeek.com/first-entry"),
                        Summary =  FeedContent.Plain("summary"),
                        PublishDate = new DateTime(2016, 08, 16),
                        Updated = new DateTime(2016, 08, 16)
                    }
                }
            };


            return feed;
        }

        [Fact]
        public void Create_a_simple_feed()
        {
            var rss20Generator = new Rss20Generator(CreateSimpleFeed());
            var feedXml = rss20Generator.Process();

            var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                           "<rss version=\"2.0\" xmlns:atom=\"http://www.w3.org/2005/Atom\">" +
                           "<channel><title>dotnetgeek feed</title>" +
                           "<atom:link rel=\"self\" type=\"application/rss+xml\" href=\"http://www.dotnetgeek.de/rss\" />" +
                           "<link>http://www.dotnetgeek.de/rss</link>" +
                           $"<lastBuildDate>Tue, 16 Aug 2016 00:00:00 {_dateTimeOffset}</lastBuildDate>" +
                           "<language>de</language>" +
                           "<copyright>2016 @ www.dotnetgeek.com</copyright>" +
                           "<generator>dng.Syndication</generator>" +
                           "<description>Dotnet relevant topics</description>" +
                           "<item>" +
                           "<title>First Entry</title>" +
                           "<guid>http://www.dotnetgeek.com/first-entry</guid>" +
                           "<link>http://www.dotnetgeek.com/first-entry</link>" +
                           "<description>Content</description>" +
                           $"<pubDate>Tue, 16 Aug 2016 00:00:00 {_dateTimeOffset}</pubDate>" +
                           "</item>" +
                           "</channel></rss>";

            Assert.Equal(expected, feedXml);
        }

        [Fact]
        public void Create_a_feed_with_image()
        {
            var feed = CreateSimpleFeed();
            feed.Image = new Image(new Uri("http://www.dotnetgeek.de/logo.png"), "dotnetgeek feed", new Uri("http://www.dotnetgeek.de"));

            var rss20Generator = new Rss20Generator(feed);
            var feedXml = rss20Generator.Process();

            var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                           "<rss version=\"2.0\" xmlns:atom=\"http://www.w3.org/2005/Atom\">" +
                           "<channel><title>dotnetgeek feed</title>" +
                           "<atom:link rel=\"self\" type=\"application/rss+xml\" href=\"http://www.dotnetgeek.de/rss\" />" +
                           "<link>http://www.dotnetgeek.de/rss</link>" +
                           $"<lastBuildDate>Tue, 16 Aug 2016 00:00:00 {_dateTimeOffset}</lastBuildDate>" +
                           "<language>de</language>" +
                           "<copyright>2016 @ www.dotnetgeek.com</copyright>" +
                           "<generator>dng.Syndication</generator>" +
                           "<description>Dotnet relevant topics</description>" +
                           "<image>" +
                             "<url>http://www.dotnetgeek.de/logo.png</url>" +
                             "<title>dotnetgeek feed</title>" +
                             "<link>http://www.dotnetgeek.de/</link>" +
                           "</image>" +
                           "<item>" +
                           "<title>First Entry</title>" +
                           "<guid>http://www.dotnetgeek.com/first-entry</guid>" +
                           "<link>http://www.dotnetgeek.com/first-entry</link>" +
                           "<description>Content</description>" +
                           $"<pubDate>Tue, 16 Aug 2016 00:00:00 {_dateTimeOffset}</pubDate>" +
                           "</item>" +
                           "</channel></rss>";

            Assert.Equal(expected, feedXml);
        }
   
   
    }
}

