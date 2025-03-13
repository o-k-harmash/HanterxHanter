using System.ComponentModel.DataAnnotations;

public class File
{
    [Key]
    [Required]
    [MaxLength(40)]
    public string FileId { get; set; }
}