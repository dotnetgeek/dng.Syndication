using System;

namespace dng.Syndication
{
    public class Author
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public Uri HomePage { get; set; }

        public Author()
        {
        }

        public Author(string name, string email = null, Uri homePage = null)
            : this()
        {
            this.Name = name;
            this.Email = email;
            this.HomePage = homePage;
        }
    }
}
