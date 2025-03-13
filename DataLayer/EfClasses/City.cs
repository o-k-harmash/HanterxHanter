using System.ComponentModel.DataAnnotations;

public class City
{
    [Key]
    [Required]
    [MaxLength(40)]
    public string CityId { get; set; }
    public string StateId { get; set; }
    public State State { get; set; }
}