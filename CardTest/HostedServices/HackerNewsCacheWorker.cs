using CardTest.Services;
using System.Formats.Asn1;
using System.Globalization;
using System.Text.Json;

namespace CardTest.HostedServices
{
    public class HackerNewsCacheWorker : BackgroundService
    {
        private ILogger<HackerNewsCacheWorker> _logger;
        private readonly IStoryService _storyService;

        public HackerNewsCacheWorker(ILogger<HackerNewsCacheWorker> logger, IStoryService storyService)
        {
            _logger = logger;
            _storyService = storyService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var response=_storyService.GetBestStories(200);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000000, stoppingToken);
            }
        }
    }
}
