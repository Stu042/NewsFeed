using System;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.Service;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Api.Functions;
public class NewsFeed {
	private readonly IProcessFeed _processFeed;
	private readonly ILogger<NewsFeed> _logger;

	public NewsFeed(IProcessFeed processFeed, ILogger<NewsFeed> logger) {
		_processFeed = processFeed;
		_logger = logger;
	}


	[FunctionName("Feed")]
	public async Task<IActionResult> Feed([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req) {
		_logger.LogInformation("NewsFeed/Feed function processed a request.");
		NewsFeedRequest newsFeedRequest;
		try {
			newsFeedRequest = NewsFeedRequest.FromQuery(req.Query);
		} catch (Exception ex) {
			return new BadRequestObjectResult(new NewsFeedResponse {
				Error = UiExceptionMessage.Message(UiExceptionMessage.Error.InputParseFailure, ex)
			});
		}
		_logger.LogInformation(newsFeedRequest.ToUiString());
		var result = await _processFeed.Run(newsFeedRequest);
		return new OkObjectResult(result);
	}
}
