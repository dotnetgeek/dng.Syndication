using System;

namespace dng.Syndication.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class AtomIgnoreAttribute : AtomPropertyAttribute
    {
        public AtomIgnoreAttribute()
        {
            Ignore = true;
        }
    }
}