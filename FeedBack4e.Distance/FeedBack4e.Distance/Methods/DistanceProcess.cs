using Newtonsoft.Json;
using FeedBack4e.Distance.Models;

namespace FeedBack4e.Distance.Methods;

public class DistanceProcess
{
  public IServiceProvider _iServiceProvider { get; set; }
  public string? airport1_response_STR { get; set; }
  public string? airport2_response_STR { get; set; }
  
  private AirportData? _airportData1, _airportData2;
  private DistanceClss _Distance;
  //-------------------------------------------------------------------------------------------------------------------
  public async Task<string> Processasync()
  {
    _airportData1 = JsonConvert.DeserializeObject<AirportData>(airport1_response_STR)!;
    _airportData2 = JsonConvert.DeserializeObject<AirportData>(airport2_response_STR)!;
    
    ICalculationDistance _calculationDistance = _iServiceProvider.GetService<ICalculationDistance>();
    _calculationDistance._Location1 = _airportData1.Location;
    _calculationDistance._Location2 = _airportData2.Location;
    
    _Distance = _calculationDistance.Calculate();
    
    return await Task.FromResult(JsonConvert.SerializeObject(_Distance));
  }
}