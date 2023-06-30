using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Api.Service {
	public class HackerNewsBase {
		protected readonly IHttpClientFactory _httpClientFactory;

		public HackerNewsBase(IHttpClientFactory httpClientFactory) {
			_httpClientFactory = httpClientFactory;
		}

		public async Task<ModelType> Call<ModelType>(string pathAddition) {
			var client = _httpClientFactory.CreateClient("HackerNews");
			var response = await client.GetAsync(client.BaseAddress + pathAddition);
			if (response.IsSuccessStatusCode) {
				var value = await response.Content.ReadAsAsync<ModelType>();
				return value;
			}
			return default;
		}
	}
}
