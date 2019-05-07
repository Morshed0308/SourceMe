using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SourceMeWebApp.Services;

namespace SourceMeWebApp.Controllers
{
    [Route("api/categories")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : Controller
    {
        private ICategoryService CategoryService { get;  }


        public CategoriesController(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Getasync()
        {
            try
            {
                var categories = CategoryService.GetAllAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                // To Do : log error
                //_logger.LogError($"Failed to get all products: {ex}");
                return BadRequest("Failed to get Categories.");
            }
        }
    }
}
