using SourceMeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceMeWebApp.Services
{
    public interface IRssService
    {
        Task<IEnumerable<FeedItem>> GetAllFeedItemsAsync(string url);
    }
}
