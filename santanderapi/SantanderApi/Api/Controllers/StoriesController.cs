using Microsoft.AspNetCore.Mvc;
using Santander.Application.Interfaces;
using System.Threading.Tasks;

namespace Santander.Api.Controllers
{
    [Route("api/stories")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IHackerApi hackerApi;

        public StoriesController(IHackerApi hackerApi)
        {
            this.hackerApi = hackerApi;
        }

        [HttpGet("/best/{number:int}")]
        public async Task<IActionResult> GetTopStories([FromRoute] int number)
        {
            List<int> lstStories = await hackerApi.GetBestStoriesAsync();

            if (number > lstStories.Count)
                return Problem(
                    title: "Error",
                    detail: "Exceed the limit size",
                    statusCode: StatusCodes.Status500InternalServerError);

            List<int> topStories = lstStories.Take(number).ToList();

            var lstTasks = topStories.Select(id => hackerApi.GetStoryById(id));

            var stories = await Task.WhenAll(lstTasks);

            return Ok(stories);
        }
        
    }
}
