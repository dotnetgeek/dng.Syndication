using dng.Syndication.Models;
using dng.Syndication.Models.Podcasts;
using System;
using System.Collections.Generic;
using System.Text;

namespace dng.Syndication.Tests.TestdataBuilder.Podcasts
{
    public class PodcastRssChannelBuilder
    {
        private List<Episode> _episodes;
        private List<Category> _categories;
        private string _title;
        private string _description;
        private string _authoremail;
        private string _type;
        private Uri _link;
        private string _language;
        private string _copyright;
        private string _generator;
        private DateTime? _date;
        private bool _explicit;
        private DateTime? _lastBuildDate;
        private string _subTitle;
        private Uri _podcastImage;
        private string _authorname;

        public PodcastRssChannelBuilder()
        {
        }

        public PodcastRssChannelBuilder WithTitle(
            string title)
        {
            _title = title;
            return this;
        }

        public PodcastRssChannelBuilder WithSubTitle(
            string subTitle)
        {
            _subTitle = subTitle;
            return this;
        }

        public PodcastRssChannelBuilder WithDescription(
            string description)
        {
            _description = description;
            return this;
        }

        public PodcastRssChannelBuilder WithAuthorName(
            string name)
        {
            _authorname = name;
            return this;
        }

        public PodcastRssChannelBuilder WithAuthorEMail(
            string email)
        {
            _authoremail = email;
            return this;
        }

        public PodcastRssChannelBuilder WithLink(
            Uri link)
        {
            _link = link;
            return this;
        }

        public PodcastRssChannelBuilder WithType(
            string type)
        {
            _type = type;
            return this;
        }

        public PodcastRssChannelBuilder WithLanguage(
            string language)
        {
            _language = language;
            return this;
        }

        public PodcastRssChannelBuilder WithCopyright(
            string copyright)
        {
            _copyright = copyright;
            return this;
        }

        public PodcastRssChannelBuilder WithGenerator(
            string generator)
        {
            _generator = generator;
            return this;
        }

        public PodcastRssChannelBuilder WithExplicit(
            bool isExplicit)
        {
            _explicit = isExplicit;
            return this;
        }

        public PodcastRssChannelBuilder WithDate(
            DateTime date)
        {
            _date = date;
            return this;
        }

        public PodcastRssChannelBuilder WithLastBuildDate(
            DateTime lastBuildDate)
        {
            _lastBuildDate = lastBuildDate;
            return this;
        }

        public PodcastRssChannelBuilder WithPodcastImage(
            Uri podcastImage)
        {
            _podcastImage = podcastImage;
            return this;
        }

        public PodcastRssChannelBuilder WithDefaultChannel()
        {
            return WithTitle("Lorem ipsum dolor sit amet?")
             .WithSubTitle("At vero eos et accusam et justo duo dolores")
             .WithLink(new Uri("https://www.example.com/podcasts/Lorem-ipsum-dolor-sit-amet"))
             .WithLanguage("en-us")
             .WithAuthorName("Lorem")
             .WithAuthorEMail("Lorem@example.com")
             .WithDescription("Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.")
             .WithType("serial");
        }

        public PodcastRssChannelBuilder WithFullChannel()
        {
            return WithTitle("Lorem ipsum dolor sit amet?")
                 .WithSubTitle("At vero eos et accusam et justo duo dolores")
                 .WithLink(new Uri("https://www.example.com/podcasts/Lorem-ipsum-dolor-sit-amet"))
                 .WithLanguage("en-us")
                 .WithCopyright("&#169; 2020 Lorem")
                 .WithGenerator("MyPodcast Generator")
                 .WithAuthorName("Lorem")
                 .WithAuthorEMail("Lorem@example.com")
                 .WithExplicit(false)
                 .WithDate(new DateTime(2020, 4, 14, 0, 3, 0))
                 .WithLastBuildDate(new DateTime(2020, 4, 13, 16, 4, 52))
                 .WithDescription("Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.")
                 .WithType("serial")
                 .WithPodcastImage(new Uri("https://www.example.com/podcasts/img/Lorem-ipsum.jpg"));
                 /*
                    .WithTitle("Lorem ipsum dolor sit amet?")
                    .WithLink(new Uri("https://www.example.com/podcasts/Lorem-ipsum-dolor-sit-amet"))
                    .WithUrl(new Uri("https://www.example.com/podcasts/img/Lorem-ipsum.jpg")
                    
                 )
                    .Build());
    */
        }

        public PodcastRssChannelBuilder WithDefaultCategories()
        {
            _categories = new List<Category>
                {
                   new Category
                   {
                       Value = "Sports",
                       SubCategories = new List<Category>
                       {
                            new Category
                            {
                                Value = "Wilderness"
                            }
                       }
                    }
                };

            return this;
        }

        public PodcastRssChannelBuilder WithEpisode(
            Episode episode)
        {
            if (_episodes == null)
                _episodes = new List<Episode>();

            _episodes.Add(episode);
            return this;
        }

        public Channel Build()
        {
            return new Channel
            {
                Title = _title,
                SubTitle = _subTitle,
                Link = _link,
                Language = _language,
                Copyright = _copyright,
                Generator = _generator,
                Author = _authorname,
                Description = _description,
                Type = _type,
                Owner = new Author() { Name = _authorname, Email = _authoremail },
                Image = _podcastImage,
                IsExplicit = _explicit,
                LastBuildDate = _lastBuildDate,
                Date = _date,
                Categories = _categories,
                Episodes = _episodes
            };
        }
    }
}
