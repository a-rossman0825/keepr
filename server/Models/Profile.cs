
using System.Text.Json.Serialization;

namespace keepr.Models
{

}

public class Profile : DbItem<string>
{
  [JsonPropertyName("sub")]
  public string Sub
  {
    get => Id;
    set => Id = value;
  }

  public string Name { get; set; }
  public string Picture { get; set; }
  public string CoverImg { get; set; }

}