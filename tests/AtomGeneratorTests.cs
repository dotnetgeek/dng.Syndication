using dng.Syndication.Generators;
using dng.Syndication.Models;
using FluentAssertions;
using Org.XmlUnit;
using Org.XmlUnit.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace dng.Syndication.Tests
{
    public class AtomGeneratorTests
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
                Description = "Dotnet relevant thinks",
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
        public void when_creating_a_simple_atom_feed_with_one_entry()
        {
            var atomGenerator = new AtomGenerator(CreateFeed(), true);
            var feedXml = atomGenerator.Process();

            var expectedInput = Input.FromFile(ExpectedContentLoader.BuildFilePath("SimpleAtomFeed.xml")).Build();

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(feedXml)))
            {
                var result = Input.FromStream(stream).Build();
                var d = DiffBuilder.Compare(expectedInput).WithTest(result).Build();

                d.HasDifferences().Should().BeFalse();
            }
        }
    }
}
