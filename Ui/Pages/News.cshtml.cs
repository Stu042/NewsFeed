using Common.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ui.Interfaces;

namespace Ui.Pages {
	public class NewsModel : PageModel {
		readonly INewsFeedService _newsFeedService;
		public NewsFeedResponse? Newsfeed;
		public string Error = String.Empty;
		public bool HighestScoreFirst = false;


		public NewsModel(INewsFeedService newsFeedService) {
			_newsFeedService = newsFeedService;
		}


		public async Task OnGet() {
			HighestScoreFirst = false;
			Newsfeed = await _newsFeedService.FetchIndex();
			if (Newsfeed == null) {
				Error = "Unknown error";
			} else if (!String.IsNullOrEmpty(Newsfeed.Error)) {
				Error = Newsfeed.Error;
			} else {
				Newsfeed.TopItems = Newsfeed.TopItems.OrderByDescending(ti => ti.Score).ToArray();
			}
		}

		public void ToggelHighestScore() {
			if (Newsfeed?.TopItems.Length > 0) {
				HighestScoreFirst = !HighestScoreFirst;
				if (HighestScoreFirst) {
					Newsfeed.TopItems = Newsfeed.TopItems.OrderBy(ti => ti.Score).ToArray();
				}else {
					Newsfeed.TopItems = Newsfeed.TopItems.OrderBy(ti => -ti.Score).ToArray();
				}
			}
		}
	}
}
