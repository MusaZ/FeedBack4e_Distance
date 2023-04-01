namespace FeedBack4e.Distance.Models;

public class IATAError
{
  public List<Errors> errors { get; set; } 
}
public class Errors
{
  public string value { get; set; }
  public string msg { get; set; }
  public string param { get; set; }
  public string location { get; set; }
}