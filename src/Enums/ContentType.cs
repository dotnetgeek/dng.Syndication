using System.ComponentModel;

namespace dng.Syndication.Enums
{
    public enum ContentType
    {
        [Description(null)]
        None,

        [Description("text")]
        Text,

        [Description("html")]
        Html,

        [Description("html")]
        Cdata
    }
}