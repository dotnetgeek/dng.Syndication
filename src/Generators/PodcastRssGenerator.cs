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
        private readonly Channel _channel;
        private readonly XNamespace itunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        private readonly XNamespace contentNamespace = "http://purl.org/rss/1.0/modules/content/";
        private readonly XNamespace googleplayNamespace = "http://www.google.com/schemas/play-podcasts/1.0";

        public PodcastRssGenerator(
            Channel channel,
            bool indentXDocument)
            : base(indentXDocument)
        {
            _channel = channel;
        }

        public string Process()
        {
            var root = new XElement("rss");

            root.Add(new XAttribute("version", "2.0"));
            root.Add(new XAttribute(XNamespace.Xmlns + "itunes", itunesNamespace));
            root.Add(new XAttribute(XNamespace.Xmlns + "googleplay", googleplayNamespace));
            root.Add(new XAttribute(XNamespace.Xmlns + "content", contentNamespace));

            CreateXDocument(root);

            var channel = new XElement("channel");
            channel.Add(new XElement("title", _channel.Title));
            channel.Add(new XElement("language", _channel.Language));

            if (_channel.Date.HasValue)
                channel.Add(new XElement("pubDate", FormatDateRfc2822(_channel.Date.Value)));

            if (_channel.LastBuildDate.HasValue)
                channel.Add(new XElement("lastBuildDate", FormatDateRfc2822(_channel.LastBuildDate.Value)));

            channel.Add(new XElement("description", _channel.Description));

            if (_channel.Link != null)
                channel.Add(new XElement("link", _channel.Link));

            channel.Add(new XElement("generator", _channel.Generator));
            channel.Add(new XElement(itunesNamespace + "type", _channel.Type));
            channel.Add(new XElement("copyright", _channel.Copyright));

            if (_channel.Image != null)
            {
                if (!IsNullOrWhiteSpace(_channel.Image.Title))
                    channel.Add(new XElement("image",
                        new XElement("url", _channel.Image.Url),
                        new XElement("title", _channel.Image.Title),
                        new XElement("link", _channel.Image.Link)));

                channel.Add(new XElement(itunesNamespace + "image", new XAttribute("href", _channel.Image.Url)));
            }

            if (!IsNullOrWhiteSpace(_channel.SubTitle))
                channel.Add(new XElement(itunesNamespace + "subtitle", _channel.SubTitle));

            channel.Add(new XElement(itunesNamespace + "author", _channel.Author));

            if (_channel.IsExplicit.HasValue)
                channel.Add(new XElement(itunesNamespace + "explicit", _channel.IsExplicit.Value ? "yes " : "no"));

            if (_channel.Categories?.Count > 0)
                AppendCatgories(channel, _channel.Categories);

            channel.Add(new XElement(itunesNamespace + "summary", _channel.Description));

            if (_channel.Owner != null)
            {
                var owner = new XElement(itunesNamespace + "owner");
                owner.Add(new XElement(itunesNamespace + "name", _channel.Owner.Name));
                owner.Add(new XElement(itunesNamespace + "email", _channel.Owner.Email));

                channel.Add(owner);
            }

            AppendItems(channel, _channel?.Episodes);

            root.Add(channel);

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
                    new XElement("pubDate", FormatDateRfc2822(episode.Date)),
                    new XElement("link", episode.Link),
                    new XElement("guid", new XAttribute("isPermaLink", false), episode.Guid),
                    new XElement(contentNamespace + "encoded", new XCData(episode.Description)),
                    episode.EpisodeNumber > 0 ? new XElement(itunesNamespace + "episode", episode.EpisodeNumber) : null,
                    new XElement(itunesNamespace + "episodeType", episode.EpisodeType.ToString().ToLower()),
                    episode.SeasonNumber > 0 ? new XElement(itunesNamespace + "season", episode.SeasonNumber) : null,
                    episode.Explicit.HasValue ? new XElement(itunesNamespace + "explicit", episode.Explicit.Value ? "yes" : "no") : null,
                    !IsNullOrWhiteSpace(episode.Author) ? new XElement(itunesNamespace + "author", episode.Author) : null,
                    new XElement("enclosure",
                        new XAttribute("url", episode.Enclosure.Url),
                        new XAttribute("type", episode.Enclosure.MediaType),
                        new XAttribute("length", episode.Enclosure.Length)),
                    new XElement(itunesNamespace + "duration", episode.Duration)
                 ));
            }
        }

        private void AppendCatgories(
            XElement parentElement,
            List<Category> categories)
        {
            foreach (var category in categories)
            {
                var categoryXElement = new XElement(itunesNamespace + "category", new XAttribute("text", category.Value));

                if (category.SubCategories?.Count > 0)
                    AppendCatgories(categoryXElement, category.SubCategories);

                parentElement.Add(categoryXElement);
            }
        }
    }
}
