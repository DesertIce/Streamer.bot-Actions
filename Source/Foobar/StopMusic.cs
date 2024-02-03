using System;
using System.Net.Http;

public class CPHInline
{
	public bool Execute()
	{
		const string apiPath = "/player/stop";
		using var client = new HttpClient();
		client.PostAsync($"http://localhost:8880/api{apiPath}", null).GetAwaiter().GetResult();
		return true;
	}
}