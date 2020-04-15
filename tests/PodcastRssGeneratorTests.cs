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

            var generator = new PodcastRssGenerator(channel, true);
            generator.Process();

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
                .Build();

            var generator = new PodcastRssGenerator(channel, true);
            var generatedRss = generator.Process();

            generatedRss.Should().Be(ExpectedContentLoader.LoadFromFile("SimplePodcastRssFeed.xml"));
        }
    }
}
