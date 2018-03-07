using System;

namespace dng.Syndication.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class AtomPropertyAttribute : BasePropertyAttribute
    {
        protected AtomPropertyAttribute()
        {
        }

        public AtomPropertyAttribute(string name)
            : base(name)
        {
        }

        public AtomPropertyAttribute(string name, string attributeName = null)
            : this(name)
        {
            AttributeName = attributeName;
        }


    }
}
