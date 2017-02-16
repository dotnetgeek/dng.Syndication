using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dng.Syndication.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public abstract class BasePropertyAttribute : Attribute
    {
        private readonly string _name = (string)null;
        private bool _ignore = false;
        private readonly string _namespace = (string)null;
        private readonly Uri _url = null;

        public string Name
        {
            get { return _name; }
        }

        public bool Ignore
        {
            get
            {
                return _ignore;
            }
            protected set
            {
                _ignore = value;
            }
        }

        public string Namespace
        {
            get
            {
                return _namespace;
            }
        }

        public Uri Url
        {
            get
            {
                return _url;
            }
        }

        public BasePropertyAttribute()
        {

        }

        public BasePropertyAttribute(string name)
            :this ()
        {
            _name = name;
        }

        public BasePropertyAttribute(string name, string url, string @namespace)
            : this(name)
        {
            _url = string.IsNullOrWhiteSpace(url) ? null : new Uri(url);
            _namespace = @namespace;
        }
    }
}
