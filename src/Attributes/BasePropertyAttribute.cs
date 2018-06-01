using System;

namespace dng.Syndication.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public abstract class BasePropertyAttribute : Attribute
    {
        public string Name { get; }

        public bool Required { get; set; }

        public bool Ignore { get; protected set; }

        public string Namespace { get; set; }

        public Uri Uri { get; private set; }

        public string AttributeName { get; set; }

        public string NamespaceUri
        {
            get => Uri?.ToString();
            set => Uri = new Uri(value);
        }

        public Type Writer { get; set; }
        
        protected BasePropertyAttribute()
        {
        }

        protected BasePropertyAttribute(string name)
            : this()
        {
            Name = name;
        }
    }
}