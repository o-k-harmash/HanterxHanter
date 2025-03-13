using System.Text.Json.Serialization;

public class ProfileLanguage
{
    public Guid ProfileId { get; set; }
    public string LanguageId { get; set; }
    [JsonIgnore]
    public Profile Profile { get; set; }

    public Language Language { get; set; }
}