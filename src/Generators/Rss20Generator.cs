using System;
using System.Xml.Linq;
using dng.Syndication.Models;
using static System.String;

namespace dng.Syndication.Generators
{
    public class RSS20Generator : Generator
    {
        private readonly Feed _feed;
        private readonly XNamespace atomNamespace = "http://www.w3.org/2005/Atom";

        public RSS20Generator(
            Feed feed,
            bool indentXDocument)
            : base(indentXDocument)
        {
            _feed = feed;
        }

        public string Process()
        {
            var root = new XElement("rss");
            root.Add(new XAttribute("version", "2.0"));
            root.Add(new XAttribute(XNamespace.Xmlns + "atom", atomNamespace));

            CreateXDocument(root);

            var channel = new XElement("channel");
            channel.Add(new XElement("title", _feed.Title));

            if (_feed.Link != null)
                channel.Add(
                    new XElement(atomNamespace + "link",
                        new XAttribute("rel", "self"),
                        new XAttribute("type", "application/rss+xml"),
                        new XAttribute("href", _feed.Link)));

            channel.Add(new XElement("link", _feed.Link));

            channel.Add(new XElement("description", _feed.Description));

            if (!IsNullOrWhiteSpace(_feed.Copyright))
                channel.Add(new XElement("copyright", _feed.Copyright));

            if (!IsNullOrWhiteSpace(_feed.Generator))
                channel.Add(new XElement("generator", _feed.Generator));

            if (!IsNullOrWhiteSpace(_feed.Generator))
                channel.Add(new XElement("language", _feed.Language));

            if (_feed.PublishedDate != DateTime.MinValue)
                channel.Add(new XElement("pubDate", FormatDateRfc2822(_feed.PublishedDate)));

            if (_feed.UpdatedDate != DateTime.MinValue)
                channel.Add(new XElement("lastBuildDate", FormatDateRfc2822(_feed.UpdatedDate)));

            if (!IsNullOrWhiteSpace(_feed.WebMaster))
                channel.Add(new XElement("webMaster", _feed.WebMaster));

            foreach (var feedEntry in _feed.FeedEntries)
            {
                var itemElement = new XElement("item");
                itemElement.Add(new XElement("title", feedEntry.Title));
                itemElement.Add(new XElement("description", feedEntry.Content));

                if (feedEntry.Author != null)
                    itemElement.Add(new XElement("author", $"{feedEntry.Author.Email} ({feedEntry.Author.Name})"));

                itemElement.Add(new XElement("guid", feedEntry.Link));

                itemElement.Add(new XElement("link", feedEntry.Link));

                if (feedEntry.PublishDate != DateTime.MinValue)
                    itemElement.Add(new XElement("pubDate", FormatDateRfc2822(feedEntry.PublishDate)));

                if (feedEntry.Enclosure != null)
                {
                    itemElement.Add(new XElement("enclosure",
                        new XAttribute("url", feedEntry.Enclosure.Url),
                        new XAttribute("length", feedEntry.Enclosure.Length),
                        new XAttribute("type", feedEntry.Enclosure.MediaType)));
                }

                channel.Add(itemElement);
            }

            root.Add(channel);

            return ConvertToString();
        }
   }
}