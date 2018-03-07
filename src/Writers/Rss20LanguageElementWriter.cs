using System;
using System.Globalization;
using System.Xml.Linq;
using dng.Syndication.Enums;

namespace dng.Syndication.Writers
{
    public class Rss20LanguageElementWriter : IElementWriter
    {
        public XElement Write(XName name, object value, string attributeName, FeedType feedType)
        {
            if (!(value is CultureInfo cultureInfo))
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} is not of Type {typeof(CultureInfo).FullName}");
            }

            return new XElement(name, cultureInfo.Name);
        }
    }
}
