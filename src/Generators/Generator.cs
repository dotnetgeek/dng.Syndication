using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using dng.Syndication.Models;
using static System.String;

namespace dng.Syndication.Generators
{
    public abstract class Generator
    {
        private static XDeclaration Declaration = new XDeclaration("1.0", "utf-8", null);

        private readonly bool _indentXDocument;
        private  XDocument _xDocument;

        public Generator(
            bool indentXDocument = false)
        {
            _indentXDocument = indentXDocument;
        }

        public XDocument XDocument => _xDocument;

        protected void CreateXDocument(
            XElement root,
            XDeclaration xDeclaration = null)
        {
            _xDocument = new XDocument(xDeclaration ?? Declaration, root);
        }

        protected static string FormatDateRfc2822(
            DateTime date)
        {
            return Concat(
                date.ToString("ddd',' d MMM yyyy HH':'mm':'ss", new CultureInfo("en-US")),
                " ",
                date.ToString("zzzz").Replace(":", ""));
        }

        protected string ConvertToString()
        {
            using (var writer = new Utf8StringWriter())
            {
                var xmlWriterSettings = new XmlWriterSettings { Indent = _indentXDocument };

                using (var xmlWriter = XmlWriter.Create(writer, xmlWriterSettings))
                    _xDocument.Save(xmlWriter);

                return writer.ToString();
            }
        }
    }
}