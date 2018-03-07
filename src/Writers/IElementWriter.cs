using System;
using System.Xml.Linq;
using dng.Syndication.Enums;

namespace dng.Syndication.Writers
{
    public interface IElementWriter
    {
        XElement Write(XName name, object value, string attributeName, FeedType feedType);
    }
}