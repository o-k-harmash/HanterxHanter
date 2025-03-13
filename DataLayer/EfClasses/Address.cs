using System.ComponentModel.DataAnnotations;

public class Address
{
    [Key]
    [Required]
    public Guid UserId { get; set; }
    public string CountryId { get; set; }
    public string CityId { get; set; }
    public string StateId { get; set; }
    // public State State { get; set; }
    // public City City { get; set; }
    // public Country Country { get; set; }
}