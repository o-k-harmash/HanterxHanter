using System.ComponentModel.DataAnnotations;

public class Country
{
    [Key]
    [Required]
    [MaxLength(40)]
    public string CountryId { get; set; }
}