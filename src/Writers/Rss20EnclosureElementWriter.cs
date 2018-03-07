using System;
using System.Xml.Linq;
using dng.Syndication.Enums;

namespace dng.Syndication.Writers
{
    public class Rss20EnclosureElementWriter : IElementWriter
    {
        public XElement Write(XName name, object value, string attributeName, FeedType feedType)
        {
            if (!(value is Enclosure enclosure))
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} is not of Type {typeof(Enclosure).FullName}");
            }

            var result = new XElement(name);
            result.SetAttributeValue("url", enclosure.Url);
            result.SetAttributeValue("length", enclosure.Length);
            result.SetAttributeValue("type", enclosure.MediaType);
            return result;
        }
    }
}