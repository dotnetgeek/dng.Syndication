using System;

namespace dng.Syndication.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class Rss20PropertyAttribute : BasePropertyAttribute
    {
        protected Rss20PropertyAttribute()
        {
        }

        public Rss20PropertyAttribute(string name)
            : base(name)
        {
        }
    }
}