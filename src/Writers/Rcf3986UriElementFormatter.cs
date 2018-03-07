using System;
using System.Linq;
using System.Xml.Linq;
using dng.Syndication.Enums;


namespace dng.Syndication.Writers
{
    public class Rcf3986UriElementFormatter : IElementWriter
    {
        public XElement Write(XName name, object value, string attributeName, FeedType feedType)
        {
            if (!(value is Uri uri))
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} is not of Type {typeof(Uri).FullName}");
            }

            var elementValue = uri.GetLeftPart(UriPartial.Authority) + string.Join("/",
                                   uri.PathAndQuery.Split(new[]
                                       {
                                           '/'
                                       }, StringSplitOptions.None)
                                       .Select(Uri.EscapeDataString));

            var result = new XElement(name);

            if (!string.IsNullOrWhiteSpace(attributeName))
            {
                result.SetAttributeValue(attributeName, elementValue);
            }
            else
            {
                result.Value = elementValue;
            }

            return result;
        }
    }
}