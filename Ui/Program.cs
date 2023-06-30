using Microsoft.Net.Http.Headers;
using Ui.Data;
using Ui.Interfaces;
using Ui.Services;

namespace Ui {
	public class Program {
		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();
			builder.Services.AddTransient<INewsFeedService, NewsFeedService>();
			var newsFeedConfig = BindConfig<NewsFeedConfig>(builder.Configuration, "NewsFeedConfig");
			builder.Services.AddHttpClient("NewsFeed", httpClient => {
				httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "NewsFeedUi");
				httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "*/*");
				httpClient.BaseAddress = new Uri(newsFeedConfig.Url);
			});
			var app = builder.Build();

			if (!app.Environment.IsDevelopment()) {
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.MapBlazorHub();
			app.MapFallbackToPage("/_Host");
			app.Run();
		}


		static ModelType BindConfig<ModelType>(IConfiguration config, string section) where ModelType : new() {
			var model = new ModelType();
			config.GetSection(section).Bind(model);
			return model;
		}
	}
}
