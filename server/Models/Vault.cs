using System.ComponentModel.DataAnnotations;

namespace keepr.Models;

public class Vault : DbItem<int>
{
  [MinLength(1), MaxLength(50)] public string Name { get; set; }
  [MinLength(1), MaxLength(1000)] public string Description { get; set; }
  [MaxLength(500), Url] public string Img { get; set; }
  public bool? IsPrivate { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
}