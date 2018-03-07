using dng.Syndication.Enums;

namespace dng.Syndication
{
    public struct FeedContent : IFeedContent
    {
        public string Value { get; set; }

        public ContentType ContentType { get; set; }

        public FeedContent(string value, ContentType contentType = ContentType.None)
        : this()
        {
            Value = value;
            ContentType = contentType;
        }

        public static FeedContent Text(string value)
        {
            return new FeedContent(value, ContentType.Text);
        }

        public static FeedContent Html(string value)
        {
            return new FeedContent(value, ContentType.Html);
        }

        public static FeedContent Plain(string value)
        {
            return new FeedContent(value);
        }

        public static FeedContent Cdata(string value)
        {
            return new FeedContent(value, ContentType.Cdata);
        }

    }
}