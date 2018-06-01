using System;

namespace dng.Syndication.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class XmlNamespaceAttribute : Attribute
    {
        public string Prefix { get; }
        public Uri Uri { get; }

        public XmlNamespaceAttribute(string prefix, string uri)
        {
            Prefix = prefix;
            Uri = new Uri(uri);
        }
    }
}