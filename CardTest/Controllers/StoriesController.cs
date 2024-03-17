using CardTest.Contract.v1;
using CardTest.Contract.v1.POCO;
using CardTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoriesController : ControllerBase
    {


        private readonly ILogger<StoriesController> _logger;
        private readonly IStoryService _storyService;

        public StoriesController(ILogger<StoriesController> logger, IStoryService storyService)
        {
            _logger = logger;
            _storyService = storyService;
        }

        
        [HttpGet(URLMapping.GetStories)]
        public async Task<ActionResult<List<GetStoriesResponse>>> GetBestStoriesAsync(int n)
        {
            try
            {
                if (n != 0)
                {
                    return Ok(await _storyService.GetBestStories(n));
                }
                else {
                    return BadRequest();
                }
                

            }catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, ex.Message);
                return BadRequest();
            }

        }
    }
}