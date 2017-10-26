using System;

namespace dng.Syndication
{
    public class Enclosure
    {
        public Enclosure(
            string url,
            int length,
            string mediaType)
        {
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(mediaType) || length < 0)
                throw new ArgumentException("Url and / or Mediatype missing.");

            Url = url;
            MediaType = mediaType;
            Length = length;
        }

        /// <summary>
        /// Where the enclosure is located
        /// </summary>
        public string Url { get; }

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