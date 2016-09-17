using System;
using System.Collections.Generic;
using dng.Syndication;
using dng.Syndication.Generators;
using Xunit;

namespace dng.Syndication.Tests
{
    public class AtomGeneratorTests
    {

        private readonly string _feedXml;

        public AtomGeneratorTests()
        {
            var atomGenerator = new AtomGenerator(CreateFeed());
            _feedXml = atomGenerator.Process();
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
                Link = new Uri("http://www.dotnetgeek.de/rss")
            };

            feed.FeedEntries = new List<FeedEntry>
            {
                new FeedEntry
                {
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
                           "<feed xmlns=\"http://www.w3.org/2005/Atom\">" +
                           "<title type=\"text\">dotnetgeek feed</title>" +
                           "<subtitle type=\"text\">Dotnet relevant thinks</subtitle>" +
                           "<id>http://www.dotnetgeek.de/rss</id>" +
                           "<link rel=\"self\" type=\"application/rss+xml\" href=\"http://www.dotnetgeek.de/rss\" />" +
                           "<author><name>Daniel</name><email>email@email.em</email></author>" +
                           "<rights>2016 @ www.dotnetgeek.com</rights>" +
                           "<generator>dng.Syndication</generator>" +
                           "<updated>2016-08-16T00:00:00Z</updated>" +
                           "<entry><title>First Entry</title>" +
                           "<link href=\"http://www.dotnetgeek.com/first-entry\" />" +
                           "<summary>summary</summary><content>Content</content>" +
                           "<author><name>Daniel</name><email>email@email.em</email></author>" +
                           "<id>http://www.dotnetgeek.com/first-entry</id>" +
                           "<updated>2016-08-16T00:00:00Z</updated>" +
                           "<published>2016-08-16T00:00:00Z</published></entry></feed>";

            Assert.Equal(expected, _feedXml);
        }
    }
}

