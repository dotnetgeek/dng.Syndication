using System.Text;
using System.Xml.Linq;
using dng.Syndication.Attributes;
using dng.Syndication.Enums;

namespace dng.Syndication.Generators
{
    public class AtomGenerator : Generator<AtomPropertyAttribute>
    {
        public AtomGenerator(Feed feed, bool indent = false) 
            : base(feed, indent)
        {
        }

        public override string Process()
        {
            var atomNamespace = XNamespace.Get(Namespaces.AtomNamespace);

            var rootElement = new XElement(atomNamespace + "feed");

            var declaration = new XDeclaration("1.0", Encoding.UTF8.HeaderName, null);

            var doc = new XDocument(declaration, rootElement);

            ParseProperties(Feed, rootElement, atomNamespace);

            return ConvertToString(doc, Indent);
        }

        public override FeedType FeedType => FeedType.Atom;
    }
}
