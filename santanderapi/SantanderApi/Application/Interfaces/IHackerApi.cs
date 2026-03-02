using Santander.Application.Dtos;

namespace Santander.Application.Interfaces
{
    public interface IHackerApi
    {
        Task<List<int>> GetBestStoriesAsync();

        Task<StoryDto> GetStoryById(int storyId);
    }
}
