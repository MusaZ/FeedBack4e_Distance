using System.ComponentModel.DataAnnotations;
using System.Net;
using FeedBack4e.Distance.Methods;
using Microsoft.AspNetCore.Mvc;
using FeedBack4e.Distance.Models;
using Newtonsoft.Json;

namespace FeedBack4e.Distance.Controllers;

[ApiController]
[Route("[controller]")]
public class DistanceCalcController : ControllerBase
{
  private IServiceProvider _iServiceProvider;
  
  private HttpResponseMessage airport1_data, airport2_data;

  private HttpClient httpclient = new HttpClient();
  
  private string? airport1_response_STR, airport2_response_STR;
  private readonly string baseIATA = "https://places-dev.cteleport.com/airports/";
  //------------------------------------------------------------------------------------------------------------------
  public DistanceCalcController(IServiceProvider iserviceprovider)
  {
    _iServiceProvider = iserviceprovider;
  }

  //------------------------------------------------------------------------------------------------------------------
  [HttpGet("Calculation")]
  public async Task<string> Calculation_Distanceasync([Required]string firstAIRPORT, [Required]string secondAIRPORT)
  {
    try {
      airport1_data = await httpclient.GetAsync($"{baseIATA}{firstAIRPORT}");
      airport2_data = await httpclient.GetAsync($"{baseIATA}{secondAIRPORT}");

      airport1_response_STR = await airport1_data.Content.ReadAsStringAsync();
      airport2_response_STR = await airport2_data.Content.ReadAsStringAsync();

      if (airport1_response_STR.IndexOf("lon") + airport2_response_STR.IndexOf("lon") > -1)
      {
        DistanceProcess distanceProcess = new()
        {
          _iServiceProvider = _iServiceProvider,
          airport1_response_STR = airport1_response_STR,
          airport2_response_STR = airport2_response_STR
        };
        return await distanceProcess.Processasync();
      }
      else if (airport1_response_STR.IndexOf("errors") + airport2_response_STR.IndexOf("errors") > -1)
      {
        HandleError handleError = new() { airport1_response_STR = airport1_response_STR };
        return await handleError.Handleasync();
      }
      else 
        return await Task.FromResult(airport1_response_STR);
    }
    catch (Exception e) {
      Hata _hata = new() { _Mesaj = $"{e.Message}", _StackTrace = $"{e.StackTrace}"};
      
      return await Task.FromResult(JsonConvert.SerializeObject(_hata));
    }
  }
}