using dng.Syndication.Models.Podcasts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using static System.String;

namespace dng.Syndication.Generators
{
    public class PodcastRssGenerator : Generator
    {
        private readonly XNamespace itunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        private readonly XNamespace contentNamespace = "http://purl.org/rss/1.0/modules/content/";
        private readonly XNamespace googleplayNamespace = "http://www.google.com/schemas/play-podcasts/1.0";

        public PodcastRssGenerator(
            bool indentXDocument)
            : base(indentXDocument)
        {
        }

        public string Process(
            Channel channel)
        {
            var root = new XElement("rss");

            root.Add(new XAttribute("version", "2.0"));
            root.Add(new XAttribute(XNamespace.Xmlns + "itunes", itunesNamespace));
            root.Add(new XAttribute(XNamespace.Xmlns + "googleplay", googleplayNamespace));
            root.Add(new XAttribute(XNamespace.Xmlns + "content", contentNamespace));

            CreateXDocument(root);

            var channelElement = new XElement("channel");
            channelElement.Add(new XElement("title", channel.Title));
            channelElement.Add(new XElement("language", channel.Language));

            if (channel.Date.HasValue)
                channelElement.Add(new XElement("pubDate", FormatDateRfc2822(channel.Date.Value)));

            if (channel.LastBuildDate.HasValue)
                channelElement.Add(new XElement("lastBuildDate", FormatDateRfc2822(channel.LastBuildDate.Value)));

            channelElement.Add(new XElement("description", channel.Description));
            channelElement.Add(new XElement(itunesNamespace + "summary", channel.Description));

            if (channel.Link != null)
                channelElement.Add(new XElement("link", channel.Link));

            if (!IsNullOrWhiteSpace(channel.Generator))
                channelElement.Add(new XElement("generator", channel.Generator));

            channelElement.Add(new XElement(itunesNamespace + "type", channel.Type));
            channelElement.Add(new XElement("copyright", channel.Copyright));

            if (channel.Image != null)
            {
                channelElement.Add(new XElement(itunesNamespace + "image", new XAttribute("href", channel.Image)));
                channelElement.Add(new XElement(googleplayNamespace + "image", new XAttribute("href", channel.Image)));
            }

            if (!IsNullOrWhiteSpace(channel.SubTitle))
                channelElement.Add(new XElement(itunesNamespace + "subtitle", channel.SubTitle));

            channelElement.Add(new XElement(itunesNamespace + "author", channel.Author));
            channelElement.Add(new XElement(googleplayNamespace + "author", channel.Author));

            if (channel.Explicit.HasValue)
            {
                channelElement.Add(new XElement(itunesNamespace + "explicit", channel.Explicit.Value ? "true " : "false"));
                channelElement.Add(new XElement(googleplayNamespace + "explicit", channel.Explicit.Value ? "yes " : "no"));
            }

            if (channel.Categories?.Count > 0)
                AppendCatgories(channelElement, channel.Categories);

            if (channel.Owner != null)
            {
                var owner = new XElement(itunesNamespace + "owner");
                owner.Add(new XElement(itunesNamespace + "name", channel.Owner.Name));
                owner.Add(new XElement(itunesNamespace + "email", channel.Owner.Email));

                channelElement.Add(owner);
            }

            if (!IsNullOrWhiteSpace(channel.Webmaster))
                channelElement.Add(new XElement("webMaster", channel.Webmaster));

            AppendItems(channelElement, channel?.Episodes);

            root.Add(channelElement);

            return ConvertToString();
        }

        private void AppendItems(
            XElement parentElement,
            List<Episode> episodes)
        {
            if (episodes == null || episodes.Count == 0)
                return;

            foreach(var episode in episodes)
            {
                parentElement.Add(
                 new XElement("item",
                    new XElement("title", episode.Title),
                    new XElement(itunesNamespace + "title", episode.Title),
                    new XElement("description", new XCData(episode.Description)),
                    new XElement(itunesNamespace + "summary", new XCData(episode.Description)),
                    new XElement("pubDate", FormatDateRfc2822(episode.Date)),
                    episode.Link != null ? new XElement("link", episode.Link) : null,
                    episode.Image != null ? new XElement(itunesNamespace + "image", new XAttribute("href", episode.Image)) : null,
                    new XElement("guid", new XAttribute("isPermaLink", false), episode.Guid),
                    new XElement(contentNamespace + "encoded", new XCData(episode.Description)),
                    episode.EpisodeNumber > 0 ? new XElement(itunesNamespace + "episode", episode.EpisodeNumber) : null,
                    new XElement(itunesNamespace + "episodeType", episode.EpisodeType.ToString().ToLower()),
                    episode.SeasonNumber > 0 ? new XElement(itunesNamespace + "season", episode.SeasonNumber) : null,
                    episode.Explicit.HasValue ? new XElement(itunesNamespace + "explicit", episode.Explicit.Value ? "true" : "false") : null,
                    episode.Explicit.HasValue ? new XElement(googleplayNamespace + "explicit", episode.Explicit.Value ? "yes" : "no") : null,
                    !IsNullOrWhiteSpace(episode.Author) ? new XElement(itunesNamespace + "author", episode.Author) : null,
                    new XElement("enclosure",
                        new XAttribute("url", episode.Enclosure.Url),
                        new XAttribute("type", episode.Enclosure.MediaType),
                        new XAttribute("length", episode.Enclosure.Length)),
                    new XElement(itunesNamespace + "duration", episode.Duration.TotalSeconds.ToString())
                 ));
            }
        }

        private void AppendCatgories(
            XElement parentElement,
            List<Category> categories)
        {
            foreach (var category in categories)
            {
                var categoryXElement = new XElement(itunesNamespace + "category", new XAttribute("text", category.Text));

                if (category.SubCategories?.Count > 0)
                    AppendCatgories(categoryXElement, category.SubCategories);

                parentElement.Add(categoryXElement);
            }
        }
    }
}
