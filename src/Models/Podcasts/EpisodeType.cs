
namespace dng.Syndication.Models.Podcasts
{
    /// <summary>
    /// If an episode is a trailer or bonus content, use this tag.
    /// https://help.apple.com/itc/podcasts_connect/#/itcb54353390
    /// </summary>
    public enum EpisodeType
    {
        /// <summary>
        ///  Specify full when you are submitting the complete content of your show
        /// </summary>
        Full,
        /// <summary>
        /// Specify trailer when you are submitting a short, promotional piece of content
        /// that represents a preview of your current show.
        /// </summary>
        Trailer,
        /// <summary>
        /// Specify bonus when you are submitting extra content for your show 
        /// (for example, behind the scenes information or interviews with the cast) 
        /// or cross-promotional content for another show.
        /// </summary>
        Bonus
    }
}
