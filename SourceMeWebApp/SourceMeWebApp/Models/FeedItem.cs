using System;

namespace SourceMeWebApp.Models
{
    public class FeedItem
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public FeedType FeedType { get; set; }
        public string ImageUrl { get; set; }

        public FeedItem()
        {
            Link = "";
            Title = "";
            Content = "";
            PublishDate = DateTime.Today;
            FeedType = FeedType.Rss;
        }
    }

    public class FeedImage
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
