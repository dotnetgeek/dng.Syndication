using System;

namespace dng.Syndication.Models.Podcasts
{
    public class Episode
    {
        public Episode()
        {
            EpisodeType = EpisodeType.Full;
        }

        /// <summary>
        /// An episode title.
        /// </summary>
        public string Title { get; set; }

        public string Subtitle{ get; set; }

        /// <summary>
        /// The episode’s globally unique identifier
        /// </summary>
        public string Guid{ get; set; }

        /// <summary>
        /// The date and time when an episode was released.
        /// </summary>
        public DateTime Date{ get; set; }

        /// <summary>
        /// An episode description.
        /// </summary>
        public string Description{ get; set; }

        public string Author { get; set; }

        /// <summary>
        /// Where the explicit value can be one of the following:
        /// </summary>
        public bool? Explicit { get; set; }

        /// <summary>
        /// Duration of the episode into seconds.
        /// </summary>
        public TimeSpan Duration{ get; set; }

        /// <summary>
        /// The episode content, file size, and file type information.
        /// </summary>
        public Enclosure Enclosure { get; set; }

        /// <summary>
        /// An episode link URL.
        /// </summary>
        public Uri Link{ get; set; }

        /// <summary>
        /// If an episode is a trailer or bonus content, use this tag.
        /// </summary>
        public EpisodeType EpisodeType { get; set; }

        /// <summary>
        /// If all your episodes have numbers and you would like them to be ordered based on 
        /// them use this tag for each one. Episode numbers are optional for <itunes:type>
        /// episodic shows, but are mandatory for serial shows.
        /// </summary>
        public int EpisodeNumber{ get; set; }

        /// <summary>
        /// The episode season number. If an episode is within a season use this tag.
        /// Where season is a non-zero integer(1, 2, 3, etc.) representing your season number.
        /// </summary>
        public int SeasonNumber{ get; set; }

        /// <summary>
        /// If you want an episode removed from the Apple directory, use this tag.
        /// </summary>
        public bool Block { get; set; }

        /// <summary>
        /// Specify your episode artwork
        /// </summary>
        public Uri Image { get; set; }
    }
}
