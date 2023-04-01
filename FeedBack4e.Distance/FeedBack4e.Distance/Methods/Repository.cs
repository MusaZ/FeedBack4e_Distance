using FeedBack4e.Distance.Models;

namespace FeedBack4e.Distance.Methods
{
	public interface IRepository
	{
		public Task<string> GetDataAsync(string? IataAirportCode);
	}

	public class Repository : IRepository
	{
		public string? IataAirportCode { get; set; }

		string baseIATA = "https://places-dev.cteleport.com/airports/";
		public async Task<string> GetDataAsync(string? IataAirportCode)
		{
			try
			{
				HttpClient httpclient = new HttpClient();
				HttpResponseMessage httpResponseMessage;

				httpResponseMessage = await httpclient.GetAsync($"{baseIATA}{IataAirportCode}");
			
				return await httpResponseMessage.Content.ReadAsStringAsync();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
	}
}
