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
  private IRepository _repository;
  
	//------------------------------------------------------------------------------------------------------------------
	public DistanceCalcController(IServiceProvider iserviceprovider, IRepository repository)
	{
		_iServiceProvider = iserviceprovider;
    _repository = repository;
  }

	//------------------------------------------------------------------------------------------------------------------
	[HttpGet("Calculation")]
  public async Task<string> Calculation_Distanceasync([Required]string firstAirport, [Required]string secondAirport)
  {
    #region VARIABLES
    HttpClient httpclient = new HttpClient();
    #endregion
    
    try
    {
      string airport1ResponseStr = await _repository.GetDataAsync(firstAirport);
      string airport2ResponseStr = await _repository.GetDataAsync(secondAirport);

      if (airport1ResponseStr.IndexOf("lon", StringComparison.Ordinal) != -1 && 
          airport2ResponseStr.IndexOf("lon", StringComparison.Ordinal) != -1)
      {
        DistanceProcess distanceProcess = new()
        {
          _iServiceProvider = _iServiceProvider,
          airport1_response_STR = airport1ResponseStr,
          airport2_response_STR = airport2ResponseStr
        };
        return await distanceProcess.Processasync();
      }
      else if (airport1ResponseStr.IndexOf("errors", StringComparison.Ordinal) != -1 || 
               airport2ResponseStr.IndexOf("errors", StringComparison.Ordinal) != -1)
      {
        HandleError handleError = new() { airport1_response_STR = airport1ResponseStr };
				return await handleError.Handleasync();
      }
      else
				return await Task.FromResult(airport1ResponseStr);      
    }
    catch (Exception e) {
      Hata hata = new() { _Mesaj = $"{e.Message}", _StackTrace = $"{e.StackTrace}"};
      
      return await Task.FromResult(JsonConvert.SerializeObject(hata));
    }
  }
}