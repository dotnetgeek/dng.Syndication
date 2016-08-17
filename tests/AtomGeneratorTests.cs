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
                           "<feed xmlns=\"http://www.w3.org/2005/Atom\">" +
                           "<title>dotnetgeek feed</title>" +
                           "<id>http://www.dotnetgeek.de/rss</id>" +
                           "<link rel=\"self\" type=\"application/rss+xml\" href=\"http://www.dotnetgeek.de/rss\" />" +
                           "<author><name>Daniel</name><email>email@email.em</email></author>" +
                           "<updated>2016-08-16T00:00:00Z</updated>" +
                           "<entry><title>First Entry</title>" +
                           "<link href=\"http://www.dotnetgeek.com/first-entry\" />" +
                           "<summary>summary</summary>" +
                           "<content>Content</content>" +
                           "<id>http://www.dotnetgeek.com/first-entry</id>" +
                           "<updated>2016-08-16T00:00:00Z</updated>" +
                           "<published>2016-08-16T00:00:00Z</published>" +
                           "</entry>" +
                           "</feed>";

            Assert.Equal(expected, _feedXml);
        }
    }
}

