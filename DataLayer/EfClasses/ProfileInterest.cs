using System.Text.Json.Serialization;

public class ProfileInterest
{
    public Guid ProfileId { get; set; }
    public string InterestId { get; set; }
    [JsonIgnore]
    public Profile Profile { get; set; }

    public Interest Interest { get; set; }
}