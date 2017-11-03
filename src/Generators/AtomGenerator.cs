using System;
using System.Xml;
using System.Xml.Linq;

namespace dng.Syndication.Generators
{
    public class AtomGenerator : Generator
    {
        private const string DateTimeRfc3339Format = "yyyy-MM-dd'T'HH:mm:ssZ";

        public AtomGenerator(
            Feed feed):base(feed)
        {
        }

        public string Process()
        {
            var ns = XNamespace.Get("http://www.w3.org/2005/Atom");

            var root = new XElement(ns + "feed");
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", null), root);

            root.Add(new XElement(ns + "title", Feed.Title, new XAttribute("type", "text")));
            root.Add(new XElement(ns + "subtitle", Feed.Description, new XAttribute("type", "text")));


            if (Feed.Link != null)
            {
                root.Add(new XElement(ns + "id", Feed.Link));
                root.Add(
                    new XElement(ns + "link",
                        new XAttribute("rel", "self"),
                        new XAttribute("type", "application/rss+xml"),
                        new XAttribute("href", Feed.Link)));
            }

            if (Feed.Author != null)
            {
                var author = new XElement(ns + "author");
                if (!string.IsNullOrWhiteSpace(Feed.Author.Name))
                    author.Add(new XElement(ns + "name", Feed.Author.Name));

                if (!string.IsNullOrWhiteSpace(Feed.Author.Name))
                    author.Add(new XElement(ns + "email", Feed.Author.Email));

                root.Add(author);
            }

            if (!string.IsNullOrWhiteSpace(Feed.Copyright))
                root.Add(new XElement(ns + "rights", Feed.Copyright));

            if (!string.IsNullOrWhiteSpace(Feed.Generator))
                root.Add(new XElement(ns + "generator", Feed.Generator));


            if (Feed.UpdatedDate != DateTime.MinValue)
                root.Add(new XElement(ns + "updated", Feed.UpdatedDate.ToString(DateTimeRfc3339Format)));

            if (Feed.PublishedDate != DateTime.MinValue)
                root.Add(new XElement(ns + "published", Feed.PublishedDate.ToString(DateTimeRfc3339Format)));

            if (Feed.Image != null)
                root.Add(new XElement(ns + "logo", Feed.Image.Url));

            foreach (var feedEntry in Feed.FeedEntries)
            {
                var itemElement = new XElement(ns + "entry");
                itemElement.Add(new XElement(ns + "title", feedEntry.Title));

                itemElement.Add(new XElement(ns + "link", new XAttribute("href", feedEntry.Link)));

                if (!string.IsNullOrWhiteSpace(feedEntry.Summary))
                    itemElement.Add(new XElement(ns + "summary", feedEntry.Summary));

                if (!string.IsNullOrWhiteSpace(feedEntry.Content))
                    itemElement.Add(new XElement(ns + "content", feedEntry.Content));

                if (feedEntry.Author != null)
                {
                    var author = new XElement(ns + "author");
                    if (!string.IsNullOrWhiteSpace(feedEntry.Author.Name))
                        author.Add(new XElement(ns + "name", feedEntry.Author.Name));

                    if (!string.IsNullOrWhiteSpace(feedEntry.Author.Name))
                        author.Add(new XElement(ns + "email", feedEntry.Author.Email));

                    itemElement.Add(author);
                }
                else if (Feed.Author != null)
                {
                    var author = new XElement(ns + "author");
                    if (!string.IsNullOrWhiteSpace(Feed.Author.Name))
                        author.Add(new XElement(ns + "name", Feed.Author.Name));

                    if (!string.IsNullOrWhiteSpace(Feed.Author.Name))
                        author.Add(new XElement(ns + "email", Feed.Author.Email));

                    itemElement.Add(author);
                }

                itemElement.Add(new XElement(ns + "id", feedEntry.Link));

                if (feedEntry.Updated != DateTime.MinValue)
                    itemElement.Add(new XElement(ns + "updated", feedEntry.Updated.ToString(DateTimeRfc3339Format)));

                if (feedEntry.PublishDate != DateTime.MinValue)
                    itemElement.Add(new XElement(ns + "published", feedEntry.PublishDate.ToString(DateTimeRfc3339Format)));

                root.Add(itemElement);
            }

            return ConvertToString(doc);
        }
    }
}
