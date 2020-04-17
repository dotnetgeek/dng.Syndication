using dng.Syndication.Models.Podcasts;
using System;
using System.Collections.Generic;
using System.Text;

namespace dng.Syndication.Tests.TestdataBuilder.Podcasts
{
    public class PodcastImageBuilder
    {
        private string _title;
        private Uri _url;
        private Uri _link;

        public PodcastImageBuilder WithTitle(
            string title)
        {
            _title = title;
            return this;
        }

        public PodcastImageBuilder WithUrl(
            Uri url)
        {
            _url = url;
            return this;
        }

        public PodcastImageBuilder WithLink(
            Uri link)
        {
            _link = link;
            return this;
        }

        public PodcastImage Build()
        {
            return new PodcastImage
            {
                Url = _url,
                Title = _title,
                Link = _link
            };
        }
    }
}
