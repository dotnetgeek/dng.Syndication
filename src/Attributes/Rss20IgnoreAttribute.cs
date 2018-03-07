using System;

namespace dng.Syndication.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class Rss20IgnoreAttribute : Rss20PropertyAttribute
    {
        public Rss20IgnoreAttribute()
        {
            Ignore = true;
        }
    }
}