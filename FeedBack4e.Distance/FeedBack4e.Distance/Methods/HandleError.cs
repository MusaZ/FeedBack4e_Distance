using Newtonsoft.Json;
using FeedBack4e.Distance.Models;

namespace FeedBack4e.Distance.Methods;

public class HandleError
{
  public string airport1_response_STR { get; set; }
  //-------------------------------------------------------------------------------------------------------------------
  public async Task<string> Handleasync()
  {
    IATAError iataError = new();
    iataError = JsonConvert.DeserializeObject<IATAError>(airport1_response_STR);

    return await Task.FromResult(JsonConvert.SerializeObject(iataError));
  }
}