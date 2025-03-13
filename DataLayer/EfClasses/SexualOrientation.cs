using System.ComponentModel.DataAnnotations;

public class SexualOrientation
{
    [Key]
    [Required]
    [MaxLength(40)]
    public string SexualOrientationId { get; set; }
}