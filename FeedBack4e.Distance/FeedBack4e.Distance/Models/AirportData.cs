namespace FeedBack4e.Distance.Models;

public class AirportData
{
  public string country { get; set; }
  public string city_iata { get; set; }
  public string iata { get; set; }
  public string city { get; set; }
  public string timezone_region_name { get; set; }
  public string country_iata { get; set; }
  public int rating { get; set; }
  public Location Location { get; set; }
  public string type { get; set; }
  public int hubs { get; set; }
}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////
public class Location
{
  public float lon { get; set; }
  public float lat { get; set; }
}