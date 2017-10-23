using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

namespace dng.Syndication.Generators
{
    public class Rss20Generator
    {
        private readonly Feed _feed;

        public Rss20Generator(
            Feed feed)
        {
            if (feed == null)
                throw new ArgumentNullException(nameof(feed));

            _feed = feed;
        }

        private string FormatDate(DateTime date)
        {
           return string.Concat(
               date.ToString("ddd',' d MMM yyyy HH':'mm':'ss", new CultureInfo("en-US")),
               " ",
               date.ToString("zzzz").Replace(":", ""));
        }

        public string Process()
        {
            XNamespace atomNamespace = "http://www.w3.org/2005/Atom";

            var doc = new XDocument(new XDeclaration("1.0", "utf-8", null), new XElement("rss"));
            doc.Root.Add(new XAttribute("version", "2.0"));
            doc.Root.Add(new XAttribute(XNamespace.Xmlns + "atom", atomNamespace));

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

            if (!string.IsNullOrWhiteSpace(_feed.Copyright))
                channel.Add(new XElement("copyright", _feed.Copyright));

            if (!string.IsNullOrWhiteSpace(_feed.Generator))
                channel.Add(new XElement("generator", _feed.Generator));

            if (!string.IsNullOrWhiteSpace(_feed.Generator))
                channel.Add(new XElement("language", _feed.Language));

            if (_feed.PublishedDate != DateTime.MinValue)
                channel.Add(new XElement("pubDate", FormatDate(_feed.PublishedDate)));

            if (_feed.UpdatedDate != DateTime.MinValue)
                channel.Add(new XElement("lastBuildDate", FormatDate(_feed.UpdatedDate)));


            doc.Root.Add(channel);

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
                    itemElement.Add(new XElement("pubDate", FormatDate(feedEntry.PublishDate)));

                if (feedEntry.Enclosure != null)
                {
                    itemElement.Add(new XElement("enclosure", 
                        new XAttribute("url", feedEntry.Enclosure.Url),
                        new XAttribute("length", feedEntry.Enclosure.Length),
                        new XAttribute("type", feedEntry.Enclosure.MediaType)));
                }

                channel.Add(itemElement);
            }

            using (var writer = new Utf8StringWriter())
            {
                var xmlWriterSettings = new XmlWriterSettings { Indent = false };

                using (var xmlWriter = XmlWriter.Create(writer, xmlWriterSettings))
                {
                    doc.Save(xmlWriter);
                }

                return writer.ToString();
            }
        }
   }
}
