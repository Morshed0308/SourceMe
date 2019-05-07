using System.Collections.Generic;
using SourceMeWebApp.Models;

namespace SourceMeWebApp.Services
{
    public interface ICategoryService
    {
        IList<Category> GetAllAsync();
        string GetChannelUrlById(int categoryId, int channelId);
    }
}
