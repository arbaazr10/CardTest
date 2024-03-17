using CardTest.Contract.v1.POCO;

namespace CardTest.Services
{
    public interface IStoryService
    {
        public Task<List<GetStoriesResponse>> GetBestStories(int n); 
    }
}
