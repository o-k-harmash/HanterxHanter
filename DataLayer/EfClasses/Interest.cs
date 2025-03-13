using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Interest
{
    [Key]
    [Required]
    [MaxLength(40)]
    public string InterestId { get; set; }

    [JsonIgnore]
    public ICollection<ProfileInterest> ProfileLinks { get; set; }
}