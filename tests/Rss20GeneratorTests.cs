using System;
using System.Collections.Generic;

using dng.Syndication.Generators;
using dng.Syndication.Models;

using Xunit;
using FluentAssertions;
using System.IO;
using System.Text;
using Org.XmlUnit.Builder;

namespace dng.Syndication.Tests
{
    public class Rss20GeneratorTests
    {
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
                FeedEntries = new List<FeedEntry>
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
                }
            };

            return feed;
        }

        [Fact]
        public void when_creating_a_simple_rss_feed_with_one_entry()
        {
            var rss20Generator = new RSS20Generator(CreateFeed(), true);
            var feedXml = rss20Generator.Process();

            var expectedInput = Input.FromFile(ExpectedContentLoader.BuildFilePath("SimpleRssFeed.xml")).Build();

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(feedXml)))
            {
                var result = Input.FromStream(stream).Build();
                var diffBuilder = DiffBuilder.Compare(expectedInput).WithTest(result).Build();

                diffBuilder.HasDifferences().Should().BeFalse();
            }
        }
    }
}