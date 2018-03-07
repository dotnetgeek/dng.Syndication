using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using dng.Syndication.Enums;

namespace dng.Syndication.Writers
{
    public class ContentElementWriter : IElementWriter
    {
        public XElement Write(XName name, object value, string attributeName, FeedType feedType)
        {
            if (!(value is IFeedContent content))
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} is not of Type {typeof(IFeedContent).FullName}");
            }

            var xelement = new XElement(name, content.Value);
            //string.IsNullOrWhiteSpace(attribute)
            //    ? (object)content.Value
            //    : (object)new XAttribute(attribute, Content));

            var attributeValue = GetDescription(content.ContentType);

            if (!string.IsNullOrWhiteSpace(attributeValue))
            {
                if (new[]
                {
                    FeedType.Atom
                }.Contains(feedType))
                {
                    xelement.SetAttributeValue("type", attributeValue);
                }
            }

            return xelement;

            //return new XElement(name, $"{author.Email}{(!string.IsNullOrWhiteSpace(author.Name) ? $" ({author.Name})" : "")}");
        }

        private static string GetDescription(ContentType enumValue, string defDesc = null)
        {
            var fi = enumValue.GetType().GetField(enumValue.ToString());

            if (null != fi)
            {
                var attrs = fi.GetCustomAttributes
                    (typeof(DescriptionAttribute), true);
                if (attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return defDesc;
        }
    }
}