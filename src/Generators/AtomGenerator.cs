﻿using System;
using System.Xml;
using System.Xml.Linq;

using dng.Syndication.Models;
using System.Web;
using System.Text;

namespace dng.Syndication.Generators
{
    public class AtomGenerator : Generator
    {
        private const string DateTimeRfc3339Format = "yyyy-MM-dd'T'HH:mm:ssZ";
        private readonly Feed _feed;

        public AtomGenerator(
            Feed feed,
            bool indentXDocument)
            : base(indentXDocument)
        {
            _feed = feed;
        }

        public string Process()
        {
            var ns = XNamespace.Get("http://www.w3.org/2005/Atom");

            var root = new XElement(ns + "feed");
            CreateXDocument(root);

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
                itemElement.Add(new XElement(ns + "title", feedEntry.Title));

                itemElement.Add(new XElement(ns + "link", 
                    new XAttribute("href", feedEntry.Link.AbsoluteUri)));

                if (!string.IsNullOrWhiteSpace(feedEntry.Summary))
                    itemElement.Add(new XElement(ns + "summary", feedEntry.Summary));

                if (!string.IsNullOrWhiteSpace(feedEntry.Content))
                    itemElement.Add(new XElement(ns + "content", new XCData(feedEntry.Content)));

                if (feedEntry.Author != null)
                {
                    var author = new XElement(ns + "author");
                    if (!string.IsNullOrWhiteSpace(feedEntry.Author.Name))
                        author.Add(new XElement(ns + "name", feedEntry.Author.Name));

                    if (!string.IsNullOrWhiteSpace(feedEntry.Author.Name))
                        author.Add(new XElement(ns + "email", feedEntry.Author.Email));

                    itemElement.Add(author);
                }
                else if (_feed.Author != null)
                {
                    var author = new XElement(ns + "author");
                    if (!string.IsNullOrWhiteSpace(_feed.Author.Name))
                        author.Add(new XElement(ns + "name", _feed.Author.Name));

                    if (!string.IsNullOrWhiteSpace(_feed.Author.Name))
                        author.Add(new XElement(ns + "email", _feed.Author.Email));

                    itemElement.Add(author);
                }

                itemElement.Add(new XElement(ns + "id", feedEntry.Link.AbsoluteUri));

                if (feedEntry.Updated != DateTime.MinValue)
                    itemElement.Add(new XElement(ns + "updated", feedEntry.Updated.ToString(DateTimeRfc3339Format)));

                if (feedEntry.PublishDate != DateTime.MinValue)
                    itemElement.Add(new XElement(ns + "published", feedEntry.PublishDate.ToString(DateTimeRfc3339Format)));

                root.Add(itemElement);
            }

            return ConvertToString();
        }
    }
}
