using System.ComponentModel.DataAnnotations;

public class RelationshipGoal
{
    [Key]
    [Required]
    [MaxLength(40)]
    public string RelationshipGoalId { get; set; }
}