using System;
using System.Collections.Generic;
using dng.Syndication.Generators;
using Xunit;

namespace dng.Syndication.Tests
{
    public class ImageTests
    {
        [Theory]
        [InlineData("http://www.dotnetgeek.de/icon.png","", "http://www.dotnetgeek.de", "Parameter Titel is required.")]
        [InlineData("","Title", "http://www.dotnetgeek.de", "Parameter Url is required.")]
         [InlineData("http://www.dotnetgeek.de/icon.png","Title", "", "Parameter Link is required.")]

        public void Check_for_required_field_title(string url, string title,string link, string expectedMessage)
        {
            var exception =  Assert.Throws<ArgumentException>(() => new Image(
                string.IsNullOrWhiteSpace(url) ? null : new Uri(url) ,
                title,
                string.IsNullOrWhiteSpace(link) ? null : new Uri(link) ));

            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}