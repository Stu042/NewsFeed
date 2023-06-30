using System;
using Api;
using Api.Interfaces;
using Api.Models;
using Api.Process;
using Api.Service;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Api;

public class Startup : FunctionsStartup {
	public override void Configure(IFunctionsHostBuilder builder) {
		var configuration = BuildConfiguration(builder.GetContext().ApplicationRootPath);
		builder.Services.AddScoped<IProcessFeed, ProcessFeed>();
		builder.Services.AddScoped<IHackerNews, HackerNews>();
		var hackerNewsApiConfig = BindConfig<HackerNewsApiConfig>(configuration, "HackerNewsApi");
		builder.Services.AddHttpClient("HackerNews", httpClient => {
			httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
			httpClient.BaseAddress = new Uri(hackerNewsApiConfig.Url);
		});
	}

	static IConfiguration BuildConfiguration(string applicationRootPath) {
		var config = new ConfigurationBuilder()
			.SetBasePath(applicationRootPath)
			.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
			.AddEnvironmentVariables()
			.Build();
		return config;
	}

	static ModelType BindConfig<ModelType>(IConfiguration config, string section) where ModelType : new() {
		var model = new ModelType();
		config.GetSection(section).Bind(model);
		return model;
	}
}
