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

        public Uri Url { get; private set; }

        public string AttributeName { get; set; }

        public string NamespaceUrl
        {
            get => Url?.ToString();
            set => Url = new Uri(value);
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