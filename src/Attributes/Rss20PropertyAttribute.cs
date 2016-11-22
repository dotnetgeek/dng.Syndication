using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dng.Syndication.Attributes
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public class Rss20PropertyAttribute : BasePropertyAttribute
    {
        /// <summary>
        /// Ignores the Property
        /// </summary>
        protected Rss20PropertyAttribute()
         : base()
        {

        }

        public Rss20PropertyAttribute(string name)
            : base(name)
        {

        }

        public Rss20PropertyAttribute(string name, string url, string @namespace)
            : base(name, url, @namespace)
        {

        }
    }
}
