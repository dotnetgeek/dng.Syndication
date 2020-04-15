using System.Collections.Generic;

namespace dng.Syndication.Models.Podcasts
{
    public class Category
    {
        public string CategoryText { get; set; }

        public string Value { get; set; }

        public List<Category> SubCategories { get; set; }
    }
}
