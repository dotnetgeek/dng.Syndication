using System.Collections.Generic;

namespace dng.Syndication.Models.Podcasts
{
    public class Category
    {
        public string Text { get; set; }

        public List<Category> SubCategories { get; set; }
    }
}
