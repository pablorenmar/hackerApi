using Microsoft.Extensions.Caching.Memory;
using Santander.Application.Dtos;
using Santander.Application.Interfaces;
using System.Collections.Concurrent;
using System.Net.Http.Json;

namespace Santander.Infrastructure.Services
{
    public class HackerApi : IHackerApi
    {
        private readonly HttpClient httpClient;
        private readonly IMemoryCache cache;

        private static readonly ConcurrentDictionary<int, Lazy<Task<StoryDto?>>> _inFlight = new();


        public HackerApi(HttpClient httpClient, IMemoryCache cache)
        {
            this.httpClient = httpClient;
            this.cache = cache;
        }

        public async Task<List<int>> GetBestStoriesAsync()
        {
            var response = await httpClient.GetFromJsonAsync<List<int>>("beststories.json");

            return response!;
        }

        public async Task<StoryDto> GetStoryById(int storyId)
        {
            var cacheKey = $"story:{storyId}";

            if (cache.TryGetValue(cacheKey, out StoryDto? cachedStory))
            {
                return cachedStory;
            }

            var response = await httpClient.GetFromJsonAsync<StoryDto>($"item/{storyId}.json");

            cache.Set(cacheKey, response, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3),
                SlidingExpiration = TimeSpan.FromMinutes(1),
                Priority = CacheItemPriority.Normal
            });

            return response!;
        }
    }
}
