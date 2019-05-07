using System.Collections.Generic;

namespace SourceMeWebApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public int NoOfChannels { get; set; }   
        public IList<Category> SubCategories { get; set; }
    }
}
