using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Language
{
    [Key]
    [Required]
    [MaxLength(40)]
    public string LanguageId { get; set; }

    [JsonIgnore]
    public ICollection<ProfileLanguage> ProfileLinks { get; set; }
}