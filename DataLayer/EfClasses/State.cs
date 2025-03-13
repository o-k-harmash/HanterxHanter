using System.ComponentModel.DataAnnotations;

public class State
{
    [Key]
    [Required]
    [MaxLength(40)]
    public string StateId { get; set; }

    public string CountryId { get; set; }
    public Country Country { get; set; }
}