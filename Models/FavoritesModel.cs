using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace proaseguros_test.Models
{
  public class FavoritesModel
  {
    public int Id { get; set; }
    public int User_id { get; set; }
    public string Fsq_id { get;set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime Created_at { get; set; }
    public DateTime? Deleted_at { get; set; }
    public FavoritesModel()
    {
      User_id = 1;
      Fsq_id = "";
      Name = "";
      Address = "";
      Created_at = DateTime.UtcNow;
      Deleted_at = null;
    }
  }
}