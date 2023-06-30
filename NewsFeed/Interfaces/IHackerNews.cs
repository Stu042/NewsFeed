using System.Threading.Tasks;
using Common.Models.HackerNews;

namespace Api.Interfaces;
public interface IHackerNews {
	Task<int?> FetchMaxItemId();
	Task<int[]> FetchNewTopBestStories();
	Task<Item> FetchItem(int storyId);
}
