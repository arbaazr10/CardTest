using CardTest.Communicator.HackerNews.POCO;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;

namespace CardTest.Communicator.HackerNews
{
    public class HackerNewsClient : IHackerNewsClient
    {
        private HttpClient _client;
        private string _url;

        public HackerNewsClient(HttpClient client, IOptions<HackerNewsOptions> options)
        {
            this._client = client;
            _url = options.Value.url;
        }

        //Gets all best Stories
        //HTTP GET
        public async Task<List<int>> GetBestStoriesAsync()
        {
            try {
                var response = await _client.GetAsync($"{_url}/v0/beststories.json");
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<int>>(responseString);
                }
                else {
                    return null;              
                }

            }
            catch (Exception ex)
            {
                throw;
            }           
        }

        //API to get detail of individual Story
        //HTTP GET
        public async Task<GetStoryResponse> GetStoryAsync(int id)
        {
            try
            {
                var response = await _client.GetAsync($"{_url}/v0/item/{id}.json");
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<GetStoryResponse>(responseString);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
