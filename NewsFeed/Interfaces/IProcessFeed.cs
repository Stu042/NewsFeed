using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using Common.Models.HackerNews;

namespace Api.Interfaces {
	public interface IProcessFeed {
		Task<NewsFeedResponse> Run(NewsFeedRequest userConfigRequest);
		IEnumerable<Item> FetchTopItems(int[] topItemIds, int count);
		IEnumerable<Item> FetchTopItemsOfType(int[] topItemIds, string itemType, int count);
	}
}