using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

namespace dng.Syndication.Generators
{
    public class Rss20Generator : Generator
    {
        public Rss20Generator(
            Feed feed) : base(feed)
        {
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
            channel.Add(new XElement("title", Feed.Title));

            if (Feed.Link != null)
                channel.Add(
                    new XElement(atomNamespace + "link", 
                        new XAttribute("rel", "self"), 
                        new XAttribute("type", "application/rss+xml"), 
                        new XAttribute("href", Feed.Link)));
            
            channel.Add(new XElement("link", Feed.Link));

            channel.Add(new XElement("description", Feed.Description));

            if (!string.IsNullOrWhiteSpace(Feed.Copyright))
                channel.Add(new XElement("copyright", Feed.Copyright));

            if (!string.IsNullOrWhiteSpace(Feed.Generator))
                channel.Add(new XElement("generator", Feed.Generator));

            if (!string.IsNullOrWhiteSpace(Feed.Generator))
                channel.Add(new XElement("language", Feed.Language));

            if (Feed.PublishedDate != DateTime.MinValue)
                channel.Add(new XElement("pubDate", FormatDate(Feed.PublishedDate)));

            if (Feed.UpdatedDate != DateTime.MinValue)
                channel.Add(new XElement("lastBuildDate", FormatDate(Feed.UpdatedDate)));
    
            if (!string.IsNullOrWhiteSpace(Feed.WebMaster))
                channel.Add(new XElement("webMaster", Feed.WebMaster));

            if (Feed.Image != null)
            {
                var imageNode = new XElement("image");
                imageNode.Add(new XElement("url", Feed.Image.Url));
                imageNode.Add(new XElement("title", Feed.Image.Title));
                imageNode.Add(new XElement("link", Feed.Image.Link));

                if (!string.IsNullOrWhiteSpace(Feed.Image.Description))
                    imageNode.Add(new XElement("description", Feed.Image.Description));

                if (Feed.Image.Height.HasValue)
                    imageNode.Add(new XElement("height", Feed.Image.Height));

                if (Feed.Image.Width.HasValue)
                    imageNode.Add(new XElement("width", Feed.Image.Width));

                channel.Add(imageNode);
            }

            doc.Root.Add(channel);

            foreach (var feedEntry in Feed.FeedEntries)
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

            return ConvertToString(doc);
        }
   }
}