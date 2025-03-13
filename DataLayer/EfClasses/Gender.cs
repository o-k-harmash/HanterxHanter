using System.ComponentModel.DataAnnotations;

public class Gender
{
    [Key]
    [Required]
    [MaxLength(40)]
    public string GenderId { get; set; }
}