using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SourceMeWebApp.Models;
using SourceMeWebApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SourceMeWebApp.Controllers
{
    [Route("api/news-feed")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NewFeedController : Controller
    {
        public NewFeedController(IRssService rssService, ICategoryService categoryService)
        {
            RssService = rssService;
            CategoryService = categoryService;
        }

        public IRssService RssService { get; }
        public ICategoryService CategoryService { get; }

        [HttpGet]
        [Route("{categoryId}/{channelId}")]
        public async Task<IActionResult> Get(int categoryId, int channelId)
        {
            try
            {
                var url = CategoryService.GetChannelUrlById(categoryId, channelId);
                var newsItems = await RssService.GetAllFeedItemsAsync(url);

                #region testdata
                //var newsItems = new List<FeedItem>()
                //{
                //    new FeedItem{Id = 1, Category = "Techonology", Title = "The top 5 personal technologies that will disrupt digital business", Description = "Personal technologies have moved from the home to the enterprise, and IT leaders must prepare for their impact.", ImageUrl = "https://www.slovenia.info/imagine_cache/og/uploads/narava/nature_slovenia_s.jpg" },

                //    new FeedItem{Id = 1, Category = "Techonology", Title = "The top 5 personal technologies that will disrupt digital business", Description = "Personal technologies have moved from the home to the enterprise, and IT leaders must prepare for their impact.", ImageUrl = "https://www.slovenia.info/imagine_cache/og/uploads/narava/nature_slovenia_s.jpg" },
                //    new FeedItem{Id = 1, Category = "Techonology", Title = "The top 5 personal technologies that will disrupt digital business", Description = "Personal technologies have moved from the home to the enterprise, and IT leaders must prepare for their impact.", ImageUrl = "https://www.slovenia.info/imagine_cache/og/uploads/narava/nature_slovenia_s.jpg" },
                //    new FeedItem{Id = 1, Category = "Techonology", Title = "The top 5 personal technologies that will disrupt digital business", Description = "Personal technologies have moved from the home to the enterprise, and IT leaders must prepare for their impact.", ImageUrl = "https://www.slovenia.info/imagine_cache/og/uploads/narava/nature_slovenia_s.jpg" },
                //    new FeedItem{Id = 1, Category = "Techonology", Title = "The top 5 personal technologies that will disrupt digital business", Description = "Personal technologies have moved from the home to the enterprise, and IT leaders must prepare for their impact.", ImageUrl = "https://www.slovenia.info/imagine_cache/og/uploads/narava/nature_slovenia_s.jpg" },
                //    new FeedItem{Id = 1, Category = "Techonology", Title = "The top 5 personal technologies that will disrupt digital business", Description = "Personal technologies have moved from the home to the enterprise, and IT leaders must prepare for their impact.", ImageUrl = "https://www.slovenia.info/imagine_cache/og/uploads/narava/nature_slovenia_s.jpg" },
                //    new FeedItem{Id = 1, Category = "Techonology", Title = "The top 5 personal technologies that will disrupt digital business", Description = "Personal technologies have moved from the home to the enterprise, and IT leaders must prepare for their impact.", ImageUrl = "https://www.slovenia.info/imagine_cache/og/uploads/narava/nature_slovenia_s.jpg" },
                //    new FeedItem{Id = 1, Category = "Techonology", Title = "The top 5 personal technologies that will disrupt digital business", Description = "Personal technologies have moved from the home to the enterprise, and IT leaders must prepare for their impact.", ImageUrl = "https://www.slovenia.info/imagine_cache/og/uploads/narava/nature_slovenia_s.jpg" },
                //    new FeedItem{Id = 1, Category = "Techonology", Title = "The top 5 personal technologies that will disrupt digital business", Description = "Personal technologies have moved from the home to the enterprise, and IT leaders must prepare for their impact.", ImageUrl = "https://www.slovenia.info/imagine_cache/og/uploads/narava/nature_slovenia_s.jpg" },
                //    new FeedItem{Id = 1, Category = "Techonology", Title = "The top 5 personal technologies that will disrupt digital business", Description = "Personal technologies have moved from the home to the enterprise, and IT leaders must prepare for their impact.", ImageUrl = "https://www.slovenia.info/imagine_cache/og/uploads/narava/nature_slovenia_s.jpg" },
                //    new FeedItem{Id = 1, Category = "Techonology", Title = "The top 5 personal technologies that will disrupt digital business", Description = "Personal technologies have moved from the home to the enterprise, and IT leaders must prepare for their impact.", ImageUrl = "https://www.slovenia.info/imagine_cache/og/uploads/narava/nature_slovenia_s.jpg" },
                //    new FeedItem{Id = 1, Category = "Techonology", Title = "The top 5 personal technologies that will disrupt digital business", Description = "Personal technologies have moved from the home to the enterprise, and IT leaders must prepare for their impact.", ImageUrl = "https://www.slovenia.info/imagine_cache/og/uploads/narava/nature_slovenia_s.jpg" },
                //}; 
                #endregion

                return Ok(newsItems);
            }
            catch (Exception ex)
            {
                // To Do : log error
                //_logger.LogError($"Failed to get all products: {ex}");
                return BadRequest("Failed to get RSS Feed.");
            }
        }
    }
}
