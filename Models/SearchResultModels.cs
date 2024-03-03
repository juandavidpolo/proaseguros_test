using System.Collections.Generic;

namespace proaseguros_test.Models
{
  public class SearchResultModel
  {
    public List<ResultModel>? Results { get; set; }
    public ContextModel? Context { get; set; }

  }

  public class ResultModel
  {
    public string Fsq_id { get; set; }
    public List<CategoryModel>? Categories { get; set; }
    public string Closed_bucket { get; set; }
    public int Distance { get; set; }
    public GeocodesModel? Geocodes { get; set; }
    public LocationModel? Location { get; set; }
    public string Name { get; set; }
    public RelatedPlacesModel? Related_places { get; set; }
    public string Timezone { get; set; }

    public ResultModel()
    {
      Fsq_id = "";
      Closed_bucket = "";
      Name = "";
      Timezone = "";
    }
  }

  public class CategoryModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Short_name { get; set; }
    public string Plural_name { get; set; }
    public IconModel? Icon { get; set; }

    public CategoryModel()
    {
      Id = 0;
      Name = "";
      Short_name = "";
      Plural_name = "";
    }
  }

  public class IconModel
  {
    public string Prefix { get; set; }
    public string Suffix { get; set; }

    public IconModel()
    {
      Prefix = "";
      Suffix = "";
    }
  }

  public class GeocodesModel
  {
    public LocationModel? Main { get; set; }
    public LocationModel? Roof { get; set; }

    }

  public class LocationModel
  {
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Address { get; set; }
    public string AdminRegion { get; set; }
    public string Country { get; set; }
    public string Cross_street { get; set; }
    public string Formatted_address { get; set; }
    public string Locality { get; set; }
    public string PostTown { get; set; }
    public string Postcode { get; set; }
    public string Region { get; set; }

    public LocationModel()
    {
      Latitude = 0;
      Longitude = 0;
      Address = "";
      AdminRegion = "";
      Country = "";
      Cross_street = "";
      Formatted_address = "";
      Locality = "";
      PostTown = "";
      Postcode = "";
      Region = "";
    }
  }

  public class RelatedPlacesModel
  {
    public List<ChildModel>? Children { get; set; }
  }

  public class ChildModel
  {
    public string Fsq_id { get; set; }
    public List<CategoryModel>? Categories { get; set; }
    public string Name { get; set; }

    public ChildModel()
    {
      Fsq_id = "";
      Name = "";
    }
  }

  public class ContextModel
  {
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public ContextModel()
    {
      Latitude = "";
      Longitude = "";
    }
  }
}
