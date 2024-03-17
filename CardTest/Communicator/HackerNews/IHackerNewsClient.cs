using CardTest.Communicator.HackerNews.POCO;

namespace CardTest.Communicator.HackerNews
{
    public interface IHackerNewsClient
    {
        public Task<List<int>> GetBestStoriesAsync();

        public Task<GetStoryResponse> GetStoryAsync(int id);
     }
}
