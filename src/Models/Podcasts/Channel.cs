using System;
using System.Collections.Generic;

namespace dng.Syndication.Models.Podcasts
{
    public class Channel
    {
        /// <summary>
        /// The show title.
        /// </summary>
        public string Title { get; set; }

        public string SubTitle { get; set; }

        /// <summary>
        /// Where description is text containing one or more sentences
        /// describing your podcast to potential listeners. The maximum amount
        /// of text allowed for this tag is 4000 characters.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Use this when an episode has a corresponding webpage.
        /// </summary>
        public Uri Link { get; set; }

        /// <summary>
        /// The <itunes:owner> tag information is for administrative communication about the podcast
        /// and isn’t displayed in Apple Podcasts. Please make sure the email address is active and monitored.
        /// </summary>
        public Author Owner { get; set; }

        /// <summary>
        /// Show author most often refers to the parent company or network of a podcast,
        /// but it can also be used to identify the host(s) if none exists.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// f your show is copyrighted you should use this tag. For example:
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// The language spoken on the show.
        /// Values must be one of the ISO 639 list (two-letter language codes,
        /// with some possible modifiers, such as "en-us")
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Specify your podcast artwork
        /// </summary>
        public Uri Image { get; set; }

        public DateTime? LastBuildDate { get; set; }

        public DateTime? Date { get; set; }

        public List<Category> Categories { get; set; }

        /// <summary>
        /// The podcast parental advisory information.
        /// </summary>
        public bool? IsExplicit { get; set; }

        public List<Episode> Episodes { get; set; }

        /// <summary>
        /// episodic or serial
        /// </summary>
        public string Type { get; set; }

        public string Generator { get; set; }
    }
}
