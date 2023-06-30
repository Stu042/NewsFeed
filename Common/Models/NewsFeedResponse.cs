using Common.Models.HackerNews;

namespace Common.Models;

public class NewsFeedResponse {
	public string Error { get; set; } = string.Empty;
	public int MaxItemId { get; set; }
	public int[] TopStoryIds { get; set; } = Array.Empty<int>();
	public Item[] TopItems { get; set; } = Array.Empty<Item>();
}
