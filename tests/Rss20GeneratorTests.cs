using System;
using System.Collections.Generic;
using dng.Syndication;
using dng.Syndication.Generators;
using Xunit;

namespace dng.Syndication.Tests
{
    public class Rss20GeneratorTests
    {

        private readonly string _feedXml;

        public Rss20GeneratorTests()
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
                Description = "Dotnet relevant thinks",
                Generator = "dng.Syndication",
                Language = "de",
                UpdatedDate = new DateTime(2016, 08, 16),
                Link = new Uri("http://www.dotnetgeek.de/rss"),
                Id = "xxxx"
            };

            feed.FeedEntries = new List<FeedEntry>
            {
                new FeedEntry
                {
                    Id ="abc",
                    Title = "First Entry",
                    Content = "Content",
                    Link = new Uri("http://www.dotnetgeek.com/first-entry"),
                    Summary = "summary",
                    PublishDate = new DateTime(2016, 08, 16),
                    Updated = new DateTime(2016, 08, 16),
                }
            };

            return feed;
        }

        [Fact]
        public void CreatedFeedIsAsExpected()
        {
            var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                           "<rss version=\"2.0\" xmlns:atom=\"http://www.w3.org/2005/Atom\">" +
                           "<channel><title>dotnetgeek feed</title>" +
                           "<atom:link rel=\"self\" type=\"application/rss+xml\" href=\"http://www.dotnetgeek.de/rss\" />" +
                           "<link>http://www.dotnetgeek.de/rss</link>" +
                           "<description>Dotnet relevant thinks</description><copyright>2016 @ www.dotnetgeek.com</copyright>" +
                           "<item><title>First Entry</title><description>Content</description>" +
                           "<guid>http://www.dotnetgeek.com/first-entry</guid>" +
                           "<pubDate>Tue, 16 Aug 2016 00:00:00 +0200</pubDate>" +
                           "</item></channel></rss>";

            Assert.Equal(expected, _feedXml);
        }
    }
}

