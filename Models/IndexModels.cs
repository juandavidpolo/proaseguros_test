namespace proaseguros_test.Models
{
  public class HomeModel
  {
    public string Location { get; set; }
    public string query { get; set;}
    public bool OpenNow { get; set; }

    public HomeModel()
    {
      Location = "";
      query = "";
      OpenNow = false;
    }
  }
}
