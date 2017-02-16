using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using dng.Syndication.Attributes;

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

        private void FormatPropertyValue(XElement root, Type type, XNamespace @namespace, string name, object value)
        {
            if (type == typeof(DateTime))
            {
                root.Add(new XElement(@namespace == null ? name : @namespace + name, FormatDate((DateTime)value)));
            }
            else if (type == typeof(Author))
            {
                root.Add(new XElement(@namespace == null ? name : @namespace + name, $"{((Author)value).Email} ({((Author)value).Name})"));
            }
            else
            {
                root.Add(new XElement(@namespace == null ? name : @namespace + name, value));
            }
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

            if (_feed.Logo != null)
            {
                var image = new XElement("image");
                if (_feed.Logo.Url != null)
                    image.Add(new XElement("url", _feed.Logo.Url));

                if (!string.IsNullOrWhiteSpace(_feed.Logo.Title))
                    image.Add(new XElement("title", _feed.Logo.Title));

                if (_feed.Logo.Link != null)
                    image.Add(new XElement("link", _feed.Logo.Link));

                if (!string.IsNullOrWhiteSpace(_feed.Logo.Description))
                    image.Add(new XElement("description", _feed.Logo.Description));

                if (_feed.Logo.Height.HasValue)
                    image.Add(new XElement("height", _feed.Logo.Height));

                if (_feed.Logo.Width.HasValue)
                    image.Add(new XElement("width", _feed.Logo.Width));

                channel.Add(image);
            }

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
                var properties = feedEntry.GetType().GetProperties();

                var itemElement = new XElement("item");

                foreach (var property in properties.Where(w => w.GetValue(feedEntry) != null))
                {
                    var attributes = property.GetCustomAttributes(typeof(Rss20PropertyAttribute), false).Cast<Rss20PropertyAttribute>();

                    //var value = FormatPropertyValue(property, property.GetValue(feedEntry));
                    var value = property.GetValue(feedEntry);

                    if (attributes != null && attributes.Any())
                    {
                        foreach (var attribute in attributes.Where(w => !w.Ignore))
                        {
                            XNamespace customNamespace = null;
                            if (!string.IsNullOrWhiteSpace(attribute.Namespace))
                            {
                                customNamespace = attribute.Url.ToString();

                                if (doc.Root.Attribute(XNamespace.Xmlns + attribute.Namespace) == null)
                                    doc.Root.Add(new XAttribute(XNamespace.Xmlns + attribute.Namespace, customNamespace));
                            }

                            FormatPropertyValue(itemElement, property.PropertyType, customNamespace, attribute.Name, value);
                        }
                    }
                    else
                    {
                        FormatPropertyValue(itemElement, property.PropertyType, null, property.Name.ToLowerInvariant(), value);
                    }
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
