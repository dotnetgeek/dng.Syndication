using System;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using dng.Syndication.Attributes;

namespace dng.Syndication.Generators
{
    public class AtomGenerator
    {
        private readonly Feed _feed;
        private const string DateTimeRfc3339Format = "yyyy-MM-dd'T'HH:mm:ssZ";

        public AtomGenerator(
            Feed feed)
        {
            if (feed == null)
                throw new ArgumentNullException(nameof(feed));

            _feed = feed;
        }

        private void FormatPropertyValue(XElement root, Feed feed, Type type, XNamespace @namespace, string name, string attribute, object value)
        {
            if (type == typeof(DateTime))
            {
                root.Add(new XElement(@namespace + name, string.IsNullOrWhiteSpace(attribute) ? (object)((DateTime)value).ToString(DateTimeRfc3339Format) : new XAttribute(attribute, ((DateTime)value).ToString(DateTimeRfc3339Format))));
            }
            else if (type == typeof(Author))
            {
                if ((Author)value == null)
                    value = feed.Author;

                if ((Author)value != null)
                {
                    var author = new XElement(@namespace + "author");
                    if (!string.IsNullOrWhiteSpace(((Author)value).Name))
                        author.Add(new XElement(@namespace + "name", ((Author)value).Name));

                    if (!string.IsNullOrWhiteSpace(((Author)value).Name))
                        author.Add(new XElement(@namespace + "email", ((Author)value).Email));

                    root.Add(author);
                }

                return;
            }
            else
            {
                root.Add(new XElement(@namespace + name, string.IsNullOrWhiteSpace(attribute) ? value : new XAttribute(attribute, value)));
            }
        }

        public string Process()
        {
            var ns = XNamespace.Get("http://www.w3.org/2005/Atom");

            var root = new XElement(ns + "feed");
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", null), root);
            
            root.Add(new XElement(ns + "title", _feed.Title, new XAttribute("type", "text")));
            root.Add(new XElement(ns + "subtitle", _feed.Description, new XAttribute("type", "text")));
            

            if (_feed.Link != null)
            {
                root.Add(new XElement(ns + "id", _feed.Link));
                root.Add(
                    new XElement(ns + "link",
                        new XAttribute("rel", "self"),
                        new XAttribute("type", "application/rss+xml"),
                        new XAttribute("href", _feed.Link)));
            }

            if (_feed.Author != null)
            {
                var author = new XElement(ns + "author");
                if (!string.IsNullOrWhiteSpace(_feed.Author.Name))
                    author.Add(new XElement(ns + "name", _feed.Author.Name));

                if (!string.IsNullOrWhiteSpace(_feed.Author.Name))
                    author.Add(new XElement(ns + "email", _feed.Author.Email));

                root.Add(author);
            }

            if (_feed.Logo != null)
            {
                if (_feed.Logo.Url != null)
                    root.Add(new XElement(ns + "logo", _feed.Logo.Url));
            }

            if (_feed.Icon != null)
            {
                if (_feed.Icon.Url != null)
                    root.Add(new XElement(ns + "icon", _feed.Icon.Url));
            }

            if (!string.IsNullOrWhiteSpace(_feed.Copyright))
                root.Add(new XElement(ns + "rights", _feed.Copyright));

            if (!string.IsNullOrWhiteSpace(_feed.Generator))
                root.Add(new XElement(ns + "generator", _feed.Generator));


            if (_feed.UpdatedDate != DateTime.MinValue)
                root.Add(new XElement(ns + "updated", _feed.UpdatedDate.ToString(DateTimeRfc3339Format)));

            if (_feed.PublishedDate != DateTime.MinValue)
                root.Add(new XElement(ns + "published", _feed.PublishedDate.ToString(DateTimeRfc3339Format)));

            foreach (var feedEntry in _feed.FeedEntries)
            {
                var itemElement = new XElement(ns + "entry");

                var properties = feedEntry.GetType().GetProperties();

                foreach (var property in properties)
                {
                    var attributes = property.GetCustomAttributes(typeof(AtomPropertyAttribute), false).Cast<AtomPropertyAttribute>();

                    var value = property.GetValue(feedEntry);

                    if (attributes != null && attributes.Any())
                    {
                        foreach (var attribute in attributes.Where(w => !w.Ignore))
                        {

                            XNamespace customNamespace = null;
                            if (!string.IsNullOrWhiteSpace(attribute.Namespace))
                            {
                                customNamespace = attribute.Url.ToString();

                                if (root.Attribute(XNamespace.Xmlns + attribute.Namespace) == null)
                                    root.Add(new XAttribute(XNamespace.Xmlns + attribute.Namespace, customNamespace));
                            }

                            FormatPropertyValue(itemElement, _feed, property.PropertyType, customNamespace ?? ns, attribute.Name, attribute.AttributeName, value);
                        }
                    }
                    else
                    {
                        FormatPropertyValue(itemElement, _feed, property.PropertyType, ns, property.Name.ToLowerInvariant(), null, value);
                    }
                }

                root.Add(itemElement);
            }

            using (var writer = new Utf8StringWriter())
            {
                var xmlWriterSettings = new XmlWriterSettings {Indent = false};

                using (var xmlWriter = XmlWriter.Create(writer, xmlWriterSettings))
                {
                    doc.Save(xmlWriter);
                }

                return writer.ToString();
            }
        }
   }
}
