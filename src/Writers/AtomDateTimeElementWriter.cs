using System;
using System.Xml.Linq;
using dng.Syndication.Enums;

namespace dng.Syndication.Writers
{
    public class AtomDateTimeElementWriter : IElementWriter
    {
        private const string DATE_TIME_RFC3339_FORMAT = "yyyy-MM-dd'T'HH:mm:ssZ";
        
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

            var formatValue = dateTime.ToString(DATE_TIME_RFC3339_FORMAT);

            return new XElement(name, formatValue);
        }
    }
}
