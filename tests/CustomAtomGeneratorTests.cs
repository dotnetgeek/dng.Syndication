using System;
using System.Collections.Generic;
using dng.Syndication;
using dng.Syndication.Attributes;
using dng.Syndication.Generators;
using Xunit;

namespace dng.Syndication.Tests
{
    public class CustomAtomGeneratorTests
    {

        private class CustomAtomFeedEntry : FeedEntry
        { 
            [AtomProperty("width", "http://www.mysever.org/2016/Picture", "picture")]
            public Int32 Width { get; set; }

            [AtomProperty("height", "http://www.mysever.org/2016/Picture", "picture")]
            public Int32 Height { get; set; }

            [AtomProperty("mimetype", "http://www.mysever.org/2016/Picture", "picture")]
            public string MimeType { get; set; }
            
            [AtomIgnore]
            public string IgnoreMe { get; set; }
        }

        private readonly string _feedXml;

        public CustomAtomGeneratorTests()
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

            feed.FeedEntries = new List<IFeedEntry>
            {
                new CustomAtomFeedEntry
                {
                    Title = "First Entry",
                    Content = "Content",
                    Link = new Uri("http://www.dotnetgeek.com/first-entry"),
                    Summary = "summary",
                    PublishDate = new DateTime(2016, 08, 16),
                    Updated = new DateTime(2016, 08, 16),
                    Height = 480,
                    Width = 640,
                    MimeType = "image/png",
                    IgnoreMe = "You won't see this"
                }
            };

            return feed;
        }

        [Fact]
        public void CreatedFeedIsAsExpected()
        {
            var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                           "<feed xmlns:picture=\"http://www.mysever.org/2016/Picture\" " + 
                           "xmlns=\"http://www.w3.org/2005/Atom\">" +
                           "<title type=\"text\">dotnetgeek feed</title>" +
                           "<subtitle type=\"text\">Dotnet relevant thinks</subtitle>" +
                           "<id>http://www.dotnetgeek.de/rss</id>" +
                           "<link rel=\"self\" type=\"application/rss+xml\" href=\"http://www.dotnetgeek.de/rss\" />" +
                           "<author><name>Daniel</name><email>email@email.em</email></author>" +
                           "<rights>2016 @ www.dotnetgeek.com</rights>" +
                           "<generator>dng.Syndication</generator>" +
                           "<updated>2016-08-16T00:00:00Z</updated>" +
                           "<entry><picture:width>640</picture:width>"+ 
                           "<picture:height>480</picture:height>" + 
                           "<picture:mimetype>image/png</picture:mimetype>" + 
                           "<title>First Entry</title>" +
                           "<link href=\"http://www.dotnetgeek.com/first-entry\" />" +
                           "<id>http://www.dotnetgeek.com/first-entry</id>" +
                           "<summary>summary</summary><content>Content</content>" +
                           "<author><name>Daniel</name><email>email@email.em</email></author>" +
                           "<published>2016-08-16T00:00:00Z</published>" +
                           "<updated>2016-08-16T00:00:00Z</updated></entry></feed>";

            Assert.Equal(expected, _feedXml);
        }
    }
}

