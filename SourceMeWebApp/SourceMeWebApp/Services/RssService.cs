using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SourceMeWebApp.Models;

namespace SourceMeWebApp.Services
{
    public class RssService : IRssService
    {
        public async Task<IEnumerable<FeedItem>> GetAllFeedItemsAsync(string url)
        {
            try
            {
                var newsItems = await ParseRssAsync(url);
                return newsItems;
            }
            catch (Exception ex)
            {
                // to do: log error
            }

            return new List<FeedItem>();
        }

        private async Task<IList<FeedItem>> ParseRssAsync(string url)
        {
            try
            {
                XDocument doc = XDocument.Load(url);

                //test
                XNamespace dcM = "http://search.yahoo.com/mrss/";
                var xdoc = XDocument.Load(url);
                var items = xdoc.Descendants("item")
                .Select(item => new FeedItem
                {
                    Title = item.Element("title").Value,
                    Content = item.Element("description").Value,
                    Link = item.Element("link").Value,
                    PublishDate = ParseDate(item.Element("pubDate").Value),

                    ImageUrl = (string)item.Elements(dcM + "content")
                    .Select(i => i.Attribute("url").Value)
                    .SingleOrDefault()
                })
                .ToList();

                // RSS/Channel/item
                //var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                //              select new FeedItem
                //              {
                //                  FeedType = FeedType.Rss,
                //                  Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                //                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                //                  PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                //                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value,
                //              };
                return items;
            }
            catch
            {
                return new List<FeedItem>();
            }
        }

        private DateTime ParseDate(string date)
        {
            DateTime result;
            if (DateTime.TryParse(date, out result))
                return result;
            else
                return DateTime.MinValue;
        }
    }
}
