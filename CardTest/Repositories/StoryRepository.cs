using CardTest.Communicator.HackerNews;
using CardTest.Communicator.HackerNews.POCO;
using CardTest.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CardTest.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private readonly IHackerNewsClient _hackerNewsClient;
        private readonly IMemoryCache _memoryCache;
        private readonly IOptions<ApplicationOptions> _applicationOptions;

        public StoryRepository(IHackerNewsClient hackerNewsClient, IMemoryCache memoryCache, IOptions<ApplicationOptions> applicationOptions)
        {
            _hackerNewsClient = hackerNewsClient;
            _memoryCache = memoryCache;
            _applicationOptions = applicationOptions;
        }

        public async Task<List<int>> GetBestStoriesAsync()
        {

            //Trying to find in cache
            List<int> getStoriesResponseString = new List<int>();

            //If not found in Cahce, fetch it from API
            bool isExist = _memoryCache.TryGetValue("allStories", out getStoriesResponseString);
            if (!isExist)
            {
                List<int> getStoryResponse = await _hackerNewsClient.GetBestStoriesAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(_applicationOptions.Value.CahcheSlidingExpiration))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(_applicationOptions.Value.CahcheAbsoluteExpiration));
                _memoryCache.Set("allStories", getStoryResponse, cacheEntryOptions);
                return getStoryResponse;
            }
            else
            {
                return getStoriesResponseString;
            }
        }


        public async Task<GetStoryResponse> GetStoryAsync(int id)
        { 


            GetStoryResponse getStoryResponseString =new GetStoryResponse();

            //Trying to find in cache
            bool isExist =  _memoryCache.TryGetValue(id.ToString(), out getStoryResponseString);
           
            //If not found in Cahce, fetch it from API
            if (!isExist)
            {
               
                GetStoryResponse getStoryResponse= await _hackerNewsClient.GetStoryAsync(id);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1))
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1));
                _memoryCache.Set(id.ToString(), getStoryResponse, cacheEntryOptions);
                return getStoryResponse;
            }
            else
            {
                return getStoryResponseString;
            }
        }
    }
}
