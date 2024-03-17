using CardTest.Communicator.HackerNews.POCO;

namespace CardTest.Repositories
{
    public interface IStoryRepository
    {
        public Task<List<int>> GetBestStoriesAsync();
        public Task<GetStoryResponse> GetStoryAsync(int id);

    }
}
