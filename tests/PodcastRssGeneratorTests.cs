using dng.Syndication.Generators;
using dng.Syndication.Models;
using dng.Syndication.Models.Podcasts;
using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using FluentAssertions;
using dng.Syndication.Tests.TestdataBuilder.Podcasts;
using System.IO;
using Org.XmlUnit.Builder;

namespace dng.Syndication.Tests
{
    public class PodcastRssGeneratorTests
    {
        [Fact]
        public void when_creating_a_podcast_rss_feed()
        {
            var channel = new PodcastRssChannelBuilder()
                .WithDefaultChannel()
                .Build();

            var generator = new PodcastRssGenerator(true);
            generator.Process(channel);

            //https://fluentassertions.com/xml/
            generator.XDocument.Declaration.Should().NotBeNull();
            generator.XDocument.Declaration.Should().BeOfType<XDeclaration>();
            generator.XDocument.Declaration.Should().BeEquivalentTo(new XDeclaration("1.0", "utf-8", null));

            generator.XDocument.Should().HaveRoot("rss").Which.Should().BeOfType<XElement>();
            generator.XDocument.Root.Should().HaveElement("channel").Which.Should().BeOfType<XElement>();
        }

        [Fact]
        public void when_creating_a_podcast_rss_feed_with_one_items()
        {
            var channel = new PodcastRssChannelBuilder()
                .WithFullChannel()
                .WithDefaultCategories()
                .WithEpisode(new PodcastRssEpisodeBuilder()
                    .WithDefault()
                    .Build())
                .WithEpisode(new PodcastRssEpisodeBuilder()
                    .WithTitle("Et accusam et justo duo")
                    .WithDescription("Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat")
                    .WithDuration(TimeSpan.FromSeconds(1102))
                    .WithId("40f0fc5c7c404")
                    .WithEpisodeType(EpisodeType.Full)
                    .WithAuthor("Justo")
                    .WithDate(new DateTime(2020, 4, 8, 12, 45, 00))
                    .WithImage(new Uri("https://www.example.com/podcasts/img/episode2.jpg"))
                    .WithEnclosure(new Enclosure
                        (
                            url: new Uri("http://example.com/podcasts/everything/AllAboutEverythingEpisode3.mp3"),
                            length: 510537,
                            mediaType: EnclosureMediaType.AudioMPEG
                        ))
                    .Build())
                .Build();

            var generator = new PodcastRssGenerator(true);
            var generatedRss = generator.Process(channel);

            var expectedInput = Input.FromFile(ExpectedContentLoader.BuildFilePath("SimplePodcastRssFeed.xml")).Build();

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(generatedRss)))
            {
                var result = Input.FromStream(stream).Build();
                var diffBuilder = DiffBuilder.Compare(expectedInput).WithTest(result).Build();

                diffBuilder.HasDifferences().Should().BeFalse();
            }
        }
    }
}
