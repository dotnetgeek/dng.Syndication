using System;
using System.Xml.Linq;
using dng.Syndication.Enums;

namespace dng.Syndication.Writers
{
    public class Rss20AuthorElementWriter : IElementWriter
    {
        public XElement Write(XName name, object value, string attributeName, FeedType feedType)
        {
            if (!(value is Author author))
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} is not of Type {typeof(Author).FullName}");
            }

            return new XElement(name, $"{author.Email}{(!string.IsNullOrWhiteSpace(author.Name) ? $" ({author.Name})" : "")}");
        }
    }
}