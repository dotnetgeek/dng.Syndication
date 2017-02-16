using System;
using System.Collections.Generic;
using System.Linq;
using dng.Syndication;
using dng.Syndication.Attributes;
using dng.Syndication.Generators;
using Xunit;

namespace dng.Syndication.Tests
{
    public class CustomRss20GeneratorTests
    {

        private class CustomRssFeedEntry : FeedEntry
        {
            [Rss20Property("url", "http://www.mysever.org/2016/Download", "download")]
            public Uri Url { get; set; }

            [Rss20Property("filesize", "http://www.mysever.org/2016/Download", "download")]
            public long FileSize { get; set; }

            [Rss20Ignore]
            public string IgnoreMe { get; set; }
        }

        private readonly string _feedXml;

        public CustomRss20GeneratorTests()
        {
            var rss20Generator = new Rss20Generator(CreateFeed());
            _feedXml = rss20Generator.Process();
        }
        
        private Feed CreateFeed()
        {
            var feed = new Feed
            {
                Title = "dotnetgeek feed",
                Author = new Author
                {
                    Name = "Daniel",
                    Email = "email@email.em"
                },
                Copyright = "2016 @ www.dotnetgeek.com",
                Description = "Dotnet relevant topics",
                Generator = "dng.Syndication",
                Language = "de",
                UpdatedDate = new DateTime(2016, 08, 16),
                Link = new Uri("http://www.dotnetgeek.de/rss"),
                FeedEntries = new List<IFeedEntry>
                {
                    new CustomRssFeedEntry
                    {
                        Title = "First Entry",
                        Content = "Content",
                        Link = new Uri("http://www.dotnetgeek.com/first-entry"),
                        Summary = "summary",
                        PublishDate = new DateTime(2016, 08, 16),
                        Updated = new DateTime(2016, 08, 16),
                        Url = new Uri("http://www.myserver.org/downloads/myfile.zip"),
                        FileSize = 123456789,
                        IgnoreMe = "You won't see this"
                    }
                }
            };


            return feed;
        }

        [Fact]
        public void CreatedFeedIsAsExpected()
        {
            var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                           "<rss version=\"2.0\" xmlns:atom=\"http://www.w3.org/2005/Atom\" "+
                           "xmlns:download=\"http://www.mysever.org/2016/Download\">" +
                           "<channel><title>dotnetgeek feed</title>" +
                           "<atom:link rel=\"self\" type=\"application/rss+xml\" href=\"http://www.dotnetgeek.de/rss\" />" +
                           "<link>http://www.dotnetgeek.de/rss</link>" +
                           "<description>Dotnet relevant topics</description>" +
                           "<copyright>2016 @ www.dotnetgeek.com</copyright>" +
                           "<generator>dng.Syndication</generator>" +
                           "<language>de</language>" +
                           "<lastBuildDate>Tue, 16 Aug 2016 00:00:00 +0200</lastBuildDate>" +
                           "<item><download:url>http://www.myserver.org/downloads/myfile.zip</download:url>" +
                           "<download:filesize>123456789</download:filesize>" + 
                           "<title>First Entry</title>" +
                           "<guid>http://www.dotnetgeek.com/first-entry</guid><link>http://www.dotnetgeek.com/first-entry</link>" +
                           "<description>Content</description><pubDate>Tue, 16 Aug 2016 00:00:00 +0200</pubDate>" +
                           "</item></channel></rss>";

            Assert.Equal(expected, _feedXml);
        }
    }
}

