using dng.Syndication.Models;
using dng.Syndication.Models.Podcasts;
using System;

namespace dng.Syndication.Tests.TestdataBuilder.Podcasts
{
    public class PodcastRssEpisodeBuilder
    {
        private EpisodeType _episodeType;
        private string _id;
        private string _title;
        private string _description;
        private string _author;
        private DateTime _date;
        private int _episodeNumber;
        private TimeSpan _duration;
        private bool _explicit;
        private Uri _link;
        private Enclosure _enclosure;
        private Uri _image;
        private string _content;

        public PodcastRssEpisodeBuilder WithEpisodeType(
            EpisodeType episodeType)
        {
            _episodeType = episodeType;
            return this;
        }

        public PodcastRssEpisodeBuilder WithId(
            string id)
        {
            _id = id;
            return this;
        }

        public PodcastRssEpisodeBuilder WithTitle(
            string title)
        {
            _title = title;
            return this;
        }

        public PodcastRssEpisodeBuilder WithDescription(
            string description)
        {
            _description = description;
            return this;
        }

        public PodcastRssEpisodeBuilder WithContent(
            string content)
        {
            _content = content;
            return this;
        }

        public PodcastRssEpisodeBuilder WithAuthor(
            string author)
        {
            _author = author;
            return this;
        }

        public PodcastRssEpisodeBuilder WithDate(
            DateTime date)
        {
            _date = date;
            return this;
        }

        public PodcastRssEpisodeBuilder WithEpisodeNumber(
            int episodeNumber)
        {
            _episodeNumber = episodeNumber;
            return this;
        }

        public PodcastRssEpisodeBuilder WithDuration(
            TimeSpan duration)
        {
            _duration = duration;
            return this;
        }

        public PodcastRssEpisodeBuilder WithExplicit(
            bool isExplicit)
        {
            _explicit = isExplicit;
            return this;
        }

        public PodcastRssEpisodeBuilder WithLink(
            Uri link)
        {
            _link = link;
            return this;
        }

        public PodcastRssEpisodeBuilder WithEnclosure(
            Enclosure enclosure)
        {
            _enclosure = enclosure;
            return this;
        }

        public PodcastRssEpisodeBuilder WithImage(
            Uri image)
        {
            _image = image;
            return this;
        }

        public PodcastRssEpisodeBuilder WithDefault()
        {
            return WithEpisodeType(EpisodeType.Full)
                .WithId("20f0fc4c7c404").WithTitle("At vero eos et accusam et justo duo")
                .WithDescription("Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum.Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.")
                .WithContent(@"<p>Lorem ipsum dolor sit amet, <a href=""https://www.apple.com/itunes/podcasts/"">consetetur sadipscing</a> elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.</p><p>At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum.<br />Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.</p>")
                .WithAuthor("Lorem")
                .WithDate(new DateTime(2020, 4, 9, 14, 45, 00))
                .WithDuration(TimeSpan.FromSeconds(1079))
                .WithExplicit(false)
                .WithLink(new Uri("https://www.example.com/podcasts/At-vero-eos-et-accusam-et-justo-duo"))
                .WithEnclosure(new Enclosure
                        (
                            url: new Uri("http://example.com/podcasts/everything/AllAboutEverythingEpisode4.mp3"),
                            length: 498537,
                            mediaType: EnclosureMediaType.AudioMPEG
                        ));
        }

        public Episode Build()
        {
            return new Episode
            {
                EpisodeType = _episodeType,
                Guid = _id,
                Title = _title,
                Description = _description,
                Content = _content,
                Author = _author,
                Date = _date,
                EpisodeNumber = _episodeNumber,
                Image = _image,
                Duration = _duration,
                Explicit = _explicit,
                Link = _link,
                Enclosure = _enclosure
            };
        }
    }
}
