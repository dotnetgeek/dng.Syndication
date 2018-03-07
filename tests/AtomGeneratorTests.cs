using System;
using System.Collections.Generic;
using System.Globalization;
using dng.Syndication.Generators;
using Xunit;

namespace dng.Syndication.Tests
{
    public class AtomGeneratorTests
    {
        private Feed CreateSimpleFeed()
        {
            var feed = new Feed
            {
                Title = FeedContent.Text("dotnetgeek feed"),
                Author = new Author
                {
                    Name = "Daniel",
                    Email = "email@email.em"
                },
                Copyright = "2016 @ www.dotnetgeek.com",
                Description = FeedContent.Text("Dotnet relevant thinks"),
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
                        Updated = new DateTime(2016, 08, 16),
                    }
                }
            };


            return feed;
        }

        [Fact]
        public void Create_a_simple_atom_feed()
        {
            var atomGenerator = new AtomGenerator(CreateSimpleFeed());
            var feedXml = atomGenerator.Process();

            const string expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                    "<feed xmlns=\"http://www.w3.org/2005/Atom\">" +
                                    "<title type=\"text\">dotnetgeek feed</title>" +
                                    "<author><name>Daniel</name><email>email@email.em</email></author>" +
                                    "<id>http://www.dotnetgeek.de/rss</id>" +
                                    "<link rel=\"self\" type=\"application/rss+xml\" href=\"http://www.dotnetgeek.de/rss\" />" +
                                    "<updated>2016-08-16T00:00:00Z</updated>" +
                                    "<rights>2016 @ www.dotnetgeek.com</rights>" +
                                    "<generator>dng.Syndication</generator>" +
                                    "<subtitle type=\"text\">Dotnet relevant thinks</subtitle>" +
                                    "<entry>" +
                                    "<title>First Entry</title>" +
                                    "<link href=\"http://www.dotnetgeek.com/first-entry\" />" +
                                    "<id>http://www.dotnetgeek.com/first-entry</id>" +
                                    "<summary>summary</summary>" +
                                    "<content>Content</content>" +
                                    "<author><name>Daniel</name><email>email@email.em</email></author>" +
                                    "<published>2016-08-16T00:00:00Z</published>" +
                                    "<updated>2016-08-16T00:00:00Z</updated>" +
                                    "</entry>" +
                                    "</feed>";

            Assert.Equal(expected, feedXml);
        }

        [Fact]
        public void Create_a_simple_atom_feed_with_logo()
        {
            var feed = CreateSimpleFeed();
            feed.Image = new Image(new Uri("http://www.dotnetgeek.de/logo.png"), "dotnetgeek feed", new Uri("http://www.dotnetgeek.de"));

            var atomGenerator = new AtomGenerator(feed);
            var feedXml = atomGenerator.Process();

            const string expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                    "<feed xmlns=\"http://www.w3.org/2005/Atom\">" +
                                    "<title type=\"text\">dotnetgeek feed</title>" +
                                    "<author><name>Daniel</name><email>email@email.em</email></author>" +
                                    "<id>http://www.dotnetgeek.de/rss</id>" +
                                    "<link rel=\"self\" type=\"application/rss+xml\" href=\"http://www.dotnetgeek.de/rss\" />" +
                                    "<updated>2016-08-16T00:00:00Z</updated>" +
                                    "<rights>2016 @ www.dotnetgeek.com</rights>" +
                                    "<generator>dng.Syndication</generator>" +
                                    "<subtitle type=\"text\">Dotnet relevant thinks</subtitle>" +
                                    "<logo>http://www.dotnetgeek.de/logo.png</logo>" +
                                    "<entry>" +
                                    "<title>First Entry</title>" +
                                    "<link href=\"http://www.dotnetgeek.com/first-entry\" />" +
                                    "<id>http://www.dotnetgeek.com/first-entry</id>" +
                                    "<summary>summary</summary>" +
                                    "<content>Content</content>" +
                                    "<author><name>Daniel</name><email>email@email.em</email></author>" +
                                    "<published>2016-08-16T00:00:00Z</published>" +
                                    "<updated>2016-08-16T00:00:00Z</updated>" +
                                    "</entry>" +
                                    "</feed>";

            Assert.Equal(expected, feedXml);
        }
    }
}

