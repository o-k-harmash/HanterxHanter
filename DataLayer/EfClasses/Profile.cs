using System.ComponentModel.DataAnnotations;

public class Profile
{
    [Key]
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string GenderId { get; set; }

    public string Bio { get; set; }

    [Required]
    public string Name { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Age cannot be less than zero.")]
    public int Age { get; set; }

    [Required]
    public string CityId { get; set; }

    [Required]
    public string SexualOrientationId { get; set; }

    public SexualOrientation SexualOrientation { get; set; }

    [Required]
    public string RelationshipGoalId { get; set; }

    public RelationshipGoal RelationshipGoal { get; set; }

    public ICollection<ProfileInterest> InterestLinks { get; set; } = new List<ProfileInterest>();

    public ICollection<ProfileLanguage> LanguageLinks { get; set; } = new List<ProfileLanguage>();

    public ICollection<File> Files { get; set; } = new List<File>();
}
