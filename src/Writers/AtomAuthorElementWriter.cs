using System;
using System.Xml.Linq;
using dng.Syndication.Enums;

namespace dng.Syndication.Writers
{
    public class AtomAuthorElementWriter : IElementWriter
    {
        public XElement Write(XName name, object value, string attributeName, FeedType feedType)
        {
            if (!(value is Author author))
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} is not of Type {typeof(DateTime).FullName}");
            }

            var authorElement = new XElement(name);
            if (!string.IsNullOrWhiteSpace(author.Name))
                authorElement.Add(new XElement(name.Namespace + "name", author.Name));
            if (!string.IsNullOrWhiteSpace(author.Email))
                authorElement.Add(new XElement(name.Namespace + "email", author.Email));
            if (author.HomePage != null)
                authorElement.Add(new XElement(name.Namespace + "uri", author.HomePage));

            return authorElement;
        }
    }
}