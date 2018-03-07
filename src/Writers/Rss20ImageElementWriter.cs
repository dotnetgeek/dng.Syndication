using System;
using System.Xml.Linq;
using dng.Syndication.Enums;

namespace dng.Syndication.Writers
{
    public class Rss20ImageElementWriter : IElementWriter
    {
        public XElement Write(XName name, object value, string attributeName, FeedType feedType)
        {
            if (value == null)
            {
                return null;
            }

            if (!(value is Image image))
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} is not of Type {typeof(Image).FullName}");
            }

            var result = new XElement(name);

            result.Add(new XElement("url", image.Url));
            result.Add(new XElement("title", image.Title));
            result.Add(new XElement("link", image.Link));

            if (!string.IsNullOrWhiteSpace(image.Description))
                result.Add(new XElement("description", image.Description));

            if (image.Height.HasValue)
            {
                result.Add(new XElement("height", image.Height));
            }

            if (image.Width.HasValue)
            {
                result.Add(new XElement("width", image.Width));
            }

            return result;
        }
    }
}