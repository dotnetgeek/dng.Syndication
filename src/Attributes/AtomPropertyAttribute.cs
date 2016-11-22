using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dng.Syndication.Attributes
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public class AtomPropertyAttribute : BasePropertyAttribute
    {
        private readonly string _attributeName = (string)null;

        public string AttributeName
        {
            get { return _attributeName; }
        }

        /// <summary>
        /// Ignores the Property
        /// </summary>
        protected AtomPropertyAttribute()
            : base()
        {

        }

        public AtomPropertyAttribute(string name)
            : base(name)
        {

        }

        public AtomPropertyAttribute(string name, string attributeName = (string)null)
            : base(name)
        {
            _attributeName = attributeName;
        }

        public AtomPropertyAttribute(string name, string url, string @namespace)
            : base(name, url, @namespace)
        {

        }

        public AtomPropertyAttribute(string name, string url, string @namespace, string attributeName = (string)null)
            : base(name, url, @namespace)
        {
            _attributeName = attributeName;
        }
    }
}
