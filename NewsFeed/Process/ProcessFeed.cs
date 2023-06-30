using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Interfaces;
using Common.Models;
using Common.Models.HackerNews;
using Microsoft.Extensions.Logging;

namespace Api.Process;

public class ProcessFeed : IProcessFeed {
	readonly IHackerNews _hackerNews;
	readonly ILogger<ProcessFeed> _logger;

	const int MAX_ITEMS = 9999;


	public ProcessFeed(IHackerNews hackerNews, ILogger<ProcessFeed> logger) {
		_hackerNews = hackerNews;
		_logger = logger;
	}


	public async Task<NewsFeedResponse> Run(NewsFeedRequest userConfigRequest) {
		var error = string.Empty;
		var maxItemId = await _hackerNews.FetchMaxItemId();
		if (maxItemId == null) {
			_logger.LogError("NewsFeed.Process.ProcessFeed/Run Unable to get HackerNews Max Item Id");
			error = "Unable to get HackerNews Max Item Id. ";
		}
		var topItemIds = await _hackerNews.FetchNewTopBestStories();
		if (topItemIds.Length == 0) {
			_logger.LogError("NewsFeed.Process.ProcessFeed/Run Unable to get HackerNews Top Stories");
			error += "Unable to get HackerNews Top Stories.";
		}
		var topItems = FetchTopItemsOfType(topItemIds, "story", MAX_ITEMS).ToArray();
		return new NewsFeedResponse {
			MaxItemId = maxItemId.Value,
			TopStoryIds = topItemIds,
			TopItems = topItems,
			Error = error
		};
	}

	public IEnumerable<Item> FetchTopItems(int[] topItemIds, int count = MAX_ITEMS) {
		count = Math.Min(count, topItemIds.Length);
		var topItemTasks = new Task<Item>[topItemIds.Length];
		for (var idx = 0; idx < topItemIds.Length; idx++) {
			var item = _hackerNews.FetchItem(topItemIds[idx]);
			topItemTasks[idx] = item;
		}
		Task.WaitAll(topItemTasks);
		var topItems = topItemTasks.Select(ti => ti.Result).Take(count);
		return topItems;
	}

	public IEnumerable<Item> FetchTopItemsOfType(int[] topItemIds, string itemType, int count) {
		var items = FetchTopItems(topItemIds);
		var topItems = items.Where(item => item.Type.ToLower() == itemType).Take(count);
		return topItems;
	}
}
