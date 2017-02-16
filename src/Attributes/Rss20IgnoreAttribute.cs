using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dng.Syndication.Attributes
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class Rss20IgnoreAttribute : Rss20PropertyAttribute
    {
        public Rss20IgnoreAttribute()
         : base()
        {
            Ignore = true;
        }
    }
}
