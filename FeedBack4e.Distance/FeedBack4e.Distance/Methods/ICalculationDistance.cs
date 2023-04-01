using FeedBack4e.Distance.Models;
using static System.Math;

namespace FeedBack4e.Distance.Methods;

public interface ICalculationDistance
{
  public Location _Location1 { get; set; }
  public Location _Location2 { get; set; }
  
  public double Distance_KM { get; set; }
  public double Distance_Miles { get; set; }
  /// <summary>
  /// Calculate Distance Between Location1 and Locaiton2
  /// </summary>
  /// <returns></returns>
  public DistanceClss Calculate();
}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
public class CalculationDistance : ICalculationDistance
{
  public Location _Location1 { get; set; }
  public Location _Location2 { get; set; }
  public double Distance_KM { get; set; }
  public double Distance_Miles { get; set; }
    
  //-------------------------------------------------------------------------------------------------------------------
  public DistanceClss Calculate()
  {
		#region VARIABLES
		double Distance_Stp1, Distance_Stp2;
	  double Lat1_Rad, Lat2_Rad, DeltaLat_Rad, DeltaLon_Rad;
		DistanceClss _distanceRESULT;
    #endregion

    try
    {
      Lat1_Rad = _Location1.lat * PI / 180;
      Lat2_Rad = _Location2.lat * PI / 180;
    
      DeltaLon_Rad = (_Location1.lon - _Location2.lon) * (PI / 180);
      DeltaLat_Rad = (_Location1.lat - _Location2.lat) * (PI / 180);

      Distance_Stp1 = Pow(Sin(DeltaLat_Rad / 2), 2) + Cos(Lat1_Rad) * Cos(Lat2_Rad) * Pow(Sin(DeltaLon_Rad / 2), 2);
      Distance_Stp2 = 2 * Asin(Sqrt(Distance_Stp1));

      Distance_KM = Distance_Stp2 * 6371.0;
      Distance_Miles = Distance_KM / 1.609344;

      _distanceRESULT = new() { Kilometers = Distance_KM, Miles = Distance_Miles };
      
      return _distanceRESULT;
    }
    catch (Exception e)
    {
      throw e;
    }
  }
}