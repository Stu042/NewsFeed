using Common.Models;
using Ui.Helper;
using Ui.Interfaces;

namespace Ui.Services;

public class NewsFeedService : INewsFeedService {
	readonly IHttpClientFactory _httpClientFactory;


	public NewsFeedService(IHttpClientFactory httpClientFactory) {
		_httpClientFactory = httpClientFactory;
	}


	public async Task<NewsFeedResponse?> FetchIndex() {
		var client = _httpClientFactory.CreateClient("NewsFeed");
		try {
			var response = await client.GetAsync(client.BaseAddress + "/Feed?name=Stu&maxItems=999");
			if (!response.IsSuccessStatusCode) {
				return new NewsFeedResponse {
					Error = "Feed failure."
				};
			}
			var modelStream = response.Content.ReadAsStream();
			var result = ModelHelper.ParseJson<NewsFeedResponse>(modelStream);
			return result;
		} catch (Exception ex) {
			return new NewsFeedResponse {
#if DEBUG
				Error = ex.Message
#else
				Error = "Unable to read news feed.");
#endif
			};
		}
	}
}
