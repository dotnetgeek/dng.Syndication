using System;
using dng.Syndication.Enums;

namespace dng.Syndication.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class InheritPropertyAttribute : Attribute
    {
        public string PropertyName { get; }

        public FeedType[] FeedTypes { get; set; } = { FeedType.Atom, FeedType.Rss20 };

        private InheritPropertyAttribute()
        {
        }

        public InheritPropertyAttribute(string propertyName )
            : this()
        {
            PropertyName = propertyName;
        }
    }
}