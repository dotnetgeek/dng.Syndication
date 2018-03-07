using System;
using System.Xml.Linq;
using dng.Syndication.Enums;

namespace dng.Syndication.Writers
{
    public class AtomImageUrlElementWriter : IElementWriter
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

            var result = new XElement(name, image.Url);
            

            return result;
        }
    }
}