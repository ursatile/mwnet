using Microsoft.Net.Http.Headers;

namespace Rockaway.WebApp.Tests;

// Based on code from https://code-maze.com/aspnet-core-testing-anti-forgery-token/
static class AntiForgeryTokenExtractor {
	public class AntiForgeryToken {
		public AntiForgeryToken(string cookieValue, string fieldValue) {
			CookieValue = cookieValue;
			FieldValue = fieldValue;
		}
		public string CookieValue { get; }
		public string FieldValue { get; }

		public void AddCookie(HttpRequestMessage request) {
			var cookie = new CookieHeaderValue(ANTI_FORGERY_COOKIE_NAME, CookieValue).ToString();
			request.Headers.Add("Cookie", cookie);
		}

		public void AddField(IDictionary<string, string> values) => values.Add(ANTI_FORGERY_FIELD_NAME, FieldValue);
	}

	public const string ANTI_FORGERY_FIELD_NAME = "AntiForgeryTokenField";
	public const string ANTI_FORGERY_COOKIE_NAME = "AntiForgeryTokenCookie";

	public static async Task<AntiForgeryToken> GetAntiForgeryTokenAsync(this HttpClient client, string url) {
		var response = await client.GetAsync("/admin/Artists/Create/");
		return response.ExtractAntiForgeryToken();
	}

	public static AntiForgeryToken ExtractAntiForgeryToken(this HttpResponseMessage response)
		=> new(ExtractCookieValue(response), ExtractAntiForgeryFieldValue(response));

	private static string ExtractCookieValue(HttpResponseMessage response)
		=> response.GetCookieValues(ANTI_FORGERY_COOKIE_NAME).First() ?? throw new($"Cookie {ANTI_FORGERY_COOKIE_NAME} not found in response");

	private static string ExtractAntiForgeryFieldValue(HttpResponseMessage response) {
		var ctx = BrowsingContext.New(Configuration.Default);
		var html = response.Content.ReadAsStringAsync().Result;
		var dom = ctx.OpenAsync(req => req.Content(html)).Result;
		var input = dom.QuerySelector($"input[name=\"{ANTI_FORGERY_FIELD_NAME}\"]");
		return input?.GetAttribute("value") ?? throw new($"Input {ANTI_FORGERY_FIELD_NAME} not found in HTML");
	}

	private static IEnumerable<string?> GetCookieValues(this HttpResponseMessage response, string cookieName) {
		var cookies = response.Headers.GetValues("Set-Cookie").Where(value => value.Contains(cookieName));
		return cookies.Select(cookie => SetCookieHeaderValue.Parse(cookie).Value.ToString());
	}

}
