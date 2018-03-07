using dng.Syndication.Enums;

namespace dng.Syndication
{
    public interface IFeedContent
    {
        string Value { get; set; }

        ContentType ContentType { get; set; }
    }
}