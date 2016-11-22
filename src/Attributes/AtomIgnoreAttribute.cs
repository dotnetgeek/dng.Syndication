using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dng.Syndication.Attributes
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class AtomIgnoreAttribute : AtomPropertyAttribute
    {
        /// <summary>
        /// Ignores the Property
        /// </summary>
        public AtomIgnoreAttribute()
            : base()
        {
            Ignore = true;
        }
    }
}
