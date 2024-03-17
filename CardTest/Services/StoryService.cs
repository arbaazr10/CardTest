using CardTest.Contract.v1.POCO;
using CardTest.Repositories;

namespace CardTest.Services
{
    public class StoryService :IStoryService
    {
        private readonly IStoryRepository _storyRepository;

        public StoryService(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public async Task<List<GetStoriesResponse>> GetBestStories(int n)
        {
            try {

                //Response Object
                List<GetStoriesResponse> getStoriesResponse = new List<GetStoriesResponse>();

                //Get All best stories
                var bestStories = await _storyRepository.GetBestStoriesAsync();

                //Loop all best stories
                foreach (var item in bestStories) {

                    //Get Individual Stories
                    var storyResponse=await _storyRepository.GetStoryAsync(item);

                    GetStoriesResponse getStories= new GetStoriesResponse();
                    getStories.title = storyResponse.title;
                    getStories.time = DateTimeOffset.FromUnixTimeSeconds(storyResponse.time).UtcDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
                    getStories.uri = storyResponse.url;
                    getStories.score = storyResponse.score;
                    getStories.postedBy = storyResponse.by;
                    getStories.commentCount = storyResponse.kids.Count;

                    getStoriesResponse.Add(getStories);


                } 
                //Sorting by score
                getStoriesResponse=getStoriesResponse.OrderByDescending(story => story.score).ToList();

                //Filtering top n rcords
                getStoriesResponse= getStoriesResponse.Take(n).ToList();

                return getStoriesResponse;

            }catch (Exception ex)
            {
                throw ex;
        
            }



        }
    }
}
