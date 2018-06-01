using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using dng.Syndication.Attributes;
using dng.Syndication.Enums;
using dng.Syndication.Writers;

namespace dng.Syndication.Generators
{
    public abstract class Generator<TAttribute> : IDisposable
        where TAttribute : BasePropertyAttribute
    {
        private readonly Dictionary<Type, IElementWriter> _elementWriters = new Dictionary<Type, IElementWriter>();

        public bool Indent { get; }

        protected readonly Feed Feed;

        protected Generator(Feed feed, bool indent = false)
        {
            Indent = indent;
            Feed = feed ?? throw new ArgumentNullException(nameof(feed));
        }

        public override string ToString()
        {
            return Process();
        }

        public abstract string Process();

        public abstract FeedType FeedType { get; }

        protected virtual void AddFeedNamespaces(XElement element)
        {
            var namespaceAttributes =
                Feed.GetType().GetCustomAttributes(typeof(XmlNamespaceAttribute), false)
                    .Cast<XmlNamespaceAttribute>().ToList();

            if (!namespaceAttributes.Any())
            {
                return;
            }

            foreach (var attribute in namespaceAttributes)
            {
                element.SetAttributeValue(XNamespace.Xmlns + attribute.Prefix, attribute.Uri);
            }
        }

        protected virtual XElement ParseProperties<TObject>(TObject obj, XElement parentElement, XNamespace @namespace = null)
            where TObject : class
        {
            return ParsePropertiesInternal(obj, (object) null, parentElement, @namespace);
        }

        internal XElement ParsePropertiesInternal<TObject, TParent>(TObject obj, TParent parentobj, XElement parentElement, XNamespace @namespace = null)
            where TObject : class
            where TParent : class
        {
            var properties = ObjectProperties(obj);

            foreach (var property in properties)
            {
                var propertyAttributes =
                    property.GetCustomAttributes(typeof(TAttribute), false)
                        .Cast<TAttribute>().ToList();

                if (!propertyAttributes.Any())
                {
                    continue;
                }

                var propertyValue = property.GetValue(obj);

                if (propertyValue == null)
                {
                    var inheritAttribute =
                        property.GetCustomAttributes(typeof(InheritPropertyAttribute), false).Cast<InheritPropertyAttribute>().FirstOrDefault();

                    if (inheritAttribute != null && inheritAttribute.FeedTypes.Contains(FeedType))
                    {
                        propertyValue = parentobj?.GetType().GetProperty(inheritAttribute.PropertyName, property.PropertyType)
                            ?.GetValue(parentobj);
                    }

                    if (propertyValue == null)
                    {
                        continue;
                    }
                }

                if (property.Name.Equals(nameof(Feed.FeedEntries), StringComparison.InvariantCultureIgnoreCase) && parentElement != null)
                {
                    foreach (var entry in (IList<IFeedEntry>) propertyValue)
                    {
                        var attribute = propertyAttributes.FirstOrDefault(w => !w.Ignore);

                        var itemElement = new XElement(GetElementName(attribute, @namespace));

                        ParsePropertiesInternal(entry, obj, itemElement, @namespace);

                        parentElement.Add(itemElement);
                    }
                }
                else
                {
                    foreach (var attribute in propertyAttributes.Where(w => !w.Ignore))
                    {
                        var elementName = GetElementName(attribute, @namespace);

                        if (attribute.Writer != null)
                        {
                            var formatter = attribute.Writer;

                            IElementWriter elementWriter;

                            if (!_elementWriters.ContainsKey(attribute.Writer))
                            {
                                elementWriter = (IElementWriter) Activator.CreateInstance(formatter);
                                _elementWriters.Add(attribute.Writer, elementWriter);
                            }
                            else
                            {
                                elementWriter = _elementWriters[attribute.Writer];
                            }

                            var writeElement = elementWriter.Write(elementName, propertyValue, attribute.AttributeName,
                                FeedType);

                            if (writeElement == null)
                            {
                                continue;
                            }

                            if (parentElement == null)
                            {
                                return writeElement;
                            }

                            parentElement.Add(writeElement);
                        }
                        else
                        {
                            var newElement = new XElement(elementName, propertyValue);

                            if (parentElement == null)
                            {
                                return newElement;
                            }

                            parentElement.Add(newElement);
                        }
                    }
                }
                
            }

            return parentElement;
        }

        private static XName GetElementName(TAttribute attribute, XNamespace @namespace)
        {
            XName result = attribute.Name;

            if (!string.IsNullOrWhiteSpace(attribute.NamespaceUri))
            {
                result = XNamespace.Get(attribute.NamespaceUri) + attribute.Name;
            }
            else if (@namespace != null)
            {
                result = @namespace + attribute.Name;
            }

            return result;
        }

        protected static string ConvertToString(XDocument doc, bool indent)
        {
            using (var writer = new Utf8StringWriter())
            {
                var xmlWriterSettings = new XmlWriterSettings { Indent = indent };

                using (var xmlWriter = XmlWriter.Create(writer, xmlWriterSettings))
                {
                    doc.Save(xmlWriter);
                }

                return writer.ToString();
            }
        }

        protected PropertyInfo[] ObjectProperties<T>(T type) where T : class 
        {
            var properties = type.GetType().GetProperties();
            Array.Sort(properties, (first, second) =>
            {
                if (first.DeclaringType.IsSubclassOf(second.DeclaringType))
                    return 1;
                return second.DeclaringType.IsSubclassOf(first.DeclaringType) ? -1 : 0;
            });
            return properties;
        }

        public void Dispose()
        {

        }
    }
}