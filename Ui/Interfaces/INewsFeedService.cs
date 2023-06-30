using Common.Models;

namespace Ui.Interfaces {
	public interface INewsFeedService {
		Task<NewsFeedResponse?> FetchIndex();
	}
}
