using System;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using dng.Syndication.Enums;

namespace dng.Syndication.Writers
{
    public class AtomUuidElementWriter : IElementWriter
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

            var elementValue = $"urn:uuid:{new Guid(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(value.ToString())))}";

            return new XElement(name, elementValue);
        }
    }
}
