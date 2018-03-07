using System;
using System.Globalization;
using System.Xml.Linq;
using dng.Syndication.Enums;

namespace dng.Syndication.Writers
{
    public class Rss20DateTimeWriter : IElementWriter
    {
        private const string RSS_DATE_TIME_FORMAT = "ddd',' d MMM yyyy HH':'mm':'ss";

        public XElement Write(XName name, object value, string attributeName, FeedType feedType)
        {
            if (!(value is DateTime dateTime))
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} is not of Type {typeof(DateTime).FullName}");
            }

            if (dateTime == DateTime.MinValue)
            {
                return null;
            }

            var formatValue = string.Concat(
                dateTime.ToString(RSS_DATE_TIME_FORMAT, new CultureInfo("en-US")),
                " ",
                dateTime.ToString("zzzz").Replace(":", ""));

            return new XElement(name, formatValue);
        }
    }
}
