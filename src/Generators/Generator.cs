using System;
using System.Xml;
using System.Xml.Linq;

namespace dng.Syndication.Generators
{
    public class Generator
    {
        protected Feed Feed { get; }

        public Generator(
            Feed feed)
        {
            Feed = feed ?? throw new ArgumentNullException(nameof(feed));
        }
        protected static string ConvertToString(
            XDocument doc)
        {
            using (var writer = new Utf8StringWriter())
            {
                var xmlWriterSettings = new XmlWriterSettings { Indent = false };

                using (var xmlWriter = XmlWriter.Create(writer, xmlWriterSettings))
                {
                    doc.Save(xmlWriter);
                }

                return writer.ToString();
            }
        }
    }
}