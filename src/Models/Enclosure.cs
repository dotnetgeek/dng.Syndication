using System;
using static System.String;

namespace dng.Syndication.Models
{
    public class Enclosure
    {
        public Enclosure(
            Uri url,
            int length,
            string mediaType)
        {
            if (url == null || IsNullOrWhiteSpace(mediaType) || length < 0)
                throw new ArgumentException("Url and / or Mediatype missing.");

            Url = url;
            MediaType = mediaType;
            Length = length;
        }

        /// <summary>
        /// Where the enclosure is located
        /// </summary>
        public Uri Url { get; }

        /// <summary>
        /// How big it is in bytes
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Standard MIME type
        /// </summary>
        public string MediaType { get; }
    }
}