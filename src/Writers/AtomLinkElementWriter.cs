using System;
using System.Xml.Linq;
using dng.Syndication.Enums;

namespace dng.Syndication.Writers
{
    public class AtomLinkElementWriter : IElementWriter
    {
        public XElement Write(XName name, object value, string attributeName, FeedType feedType)
        {
            if (value == null)
            {
                return null;
            }

            if (!(value is Uri uri))
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} is not of Type {typeof(Uri).FullName}");
            }

            return new XElement(name,
                new XAttribute("rel", "self"),
                new XAttribute("type", MediaTypes.RssMediaType),
                new XAttribute("href", uri));
        }
    }
}