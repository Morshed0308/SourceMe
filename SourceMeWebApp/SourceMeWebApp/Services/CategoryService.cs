using SourceMeWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SourceMeWebApp.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly List<Channel> _channels = new List<Channel>
        {
            new Channel
            {
                Id = 0,
                CategoryId = 2,
                Url= "http://feeds.nytimes.com/nyt/rss/Technology"
            },
            new Channel
            {
                Id = 1,
                CategoryId = 2,
                Url= "https://www.techrepublic.com/rssfeeds/topic/internet-of-things/"
            },
            new Channel
            {
                Id = 2,
                CategoryId = 3,
                Url= "https://www.techrepublic.com/rssfeeds/topic/innovation"
            },
            new Channel
            {
                Id = 3,
                CategoryId = 3,
                Url= "https://www.techrepublic.com/rssfeeds/topic/tech-and-work/"
            },
            new Channel
            {
                Id = 4,
                CategoryId = 4,
                Url= "https://www.techrepublic.com/rssfeeds/topic/tech-industry"
            },
            new Channel
            {
                Id = 5,
                CategoryId = 6,
                Url= "https://yogalondon.net/monkey/feed"
            },
            new Channel
            {
                Id = 6,
                CategoryId = 2,
                Url= ""
            },
            new Channel
            {
                Id = 7,
                CategoryId = 7,
                Url= "http://www.coutureusa.com/blog/feed"
            },
            new Channel
            {
                Id = 8,
                CategoryId = 7,
                Url= "http://www.vervemagazine.in/fashion-and-beauty/feed"
            },
            new Channel
            {
                Id = 9,
                CategoryId = 8,
                Url= "https://www.trendhunter.com/rss/category/Hip-Fashion-Trends"
            },
            new Channel
            {
                Id = 10,
                CategoryId = 8,
                Url= "http://www.styledumonde.com/feed"
            },
            new Channel
            {
                Id = 11,
                CategoryId = 8,
                Url= "https://www.reddit.com/r/streetwear/.rss?format=xml"
            }
        };
        private readonly List<Category> _categories = new List<Category>
        {
            new Category()
            {
                Id = 1,
                Name = "Technology",
                SubCategories = new List<Category>()
                {
                    new Category
                    {
                        Id = 2,
                        Name = "Social Media",
                        NoOfChannels = 2
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "Hardware",
                        NoOfChannels = 2
                    },
                    new Category
                    {
                        Id = 4,
                        Name = "Software",
                        NoOfChannels = 1
                    }
                }
            },
            new Category()
            {
                Id = 5,
                Name = "Fashion",
                SubCategories = new List<Category>
                {
                    new Category
                    {
                        Id = 6,
                        Name = "Yoga",
                        NoOfChannels = 1
                    },
                    new Category
                    {
                        Id = 7,
                        Name = "Couture",
                        NoOfChannels = 2
                    },
                    new Category
                    {
                        Id = 8,
                        Name = "High Street",
                        NoOfChannels = 3
                    }
                }
            }
        };

        public IList<Category> GetAllAsync()
        {
            return _categories;
        }

        public string GetChannelUrlById(int categoryId, int channelId)
        {
            var channels = _channels.Where(a => a.CategoryId == categoryId).ToList();
            return channels[channelId]?.Url ?? "";
        }
    }
}
