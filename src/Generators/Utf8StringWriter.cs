using System.IO;
using System.Text;

namespace dng.Syndication.Generators
{
    internal class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
