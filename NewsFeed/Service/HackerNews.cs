using System.Net.Http;
using System.Threading.Tasks;
using Api.Interfaces;
using Common.Models.HackerNews;
using Microsoft.Extensions.Logging;

namespace Api.Service;



public class HackerNews : HackerNewsBase, IHackerNews {
	protected readonly ILogger<HackerNews> _logger;
	public HackerNews(IHttpClientFactory httpClientFactory, ILogger<HackerNews> logger) : base(httpClientFactory) {
		_logger = logger;
	}

	public async Task<int?> FetchMaxItemId() {
		var result = await Call<int?>("/maxitem.json");
		return result;
	}

	public async Task<int[]> FetchNewTopBestStories() {
		var result = await Call<int[]>("/topstories.json");
		return result;
	}

	public async Task<Item> FetchItem(int storyId) {
		var result = await Call<Item>($"/item/{storyId}.json");
		return result;
	}
}
