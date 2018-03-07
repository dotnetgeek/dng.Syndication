using System.Text;
using System.Xml.Linq;
using dng.Syndication.Attributes;
using dng.Syndication.Enums;

namespace dng.Syndication.Generators
{
    public class Rss20Generator : Generator<Rss20PropertyAttribute>
    {
        public Rss20Generator(Feed feed, bool indent = false) 
            : base(feed, indent)
        {
        }

        public override string Process()
        {
            var atomNamespace = XNamespace.Get(Namespaces.AtomNamespace);

            var rootElement = new XElement("rss");

            var declaration = new XDeclaration("1.0", Encoding.UTF8.HeaderName, null);

            var doc = new XDocument(declaration, rootElement);

            rootElement.Add(new XAttribute("version", "2.0"));

            rootElement.Add(new XAttribute(XNamespace.Xmlns + "atom", atomNamespace));

            var parentElement = new XElement("channel");

            rootElement.Add(ParseProperties(Feed, parentElement));

            return ConvertToString(doc, Indent);
        }

        public override FeedType FeedType => FeedType.Rss20;
    }
}