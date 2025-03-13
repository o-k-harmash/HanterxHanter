namespace HxH.Dtos
{
    public record ProfileDtoForUpdate(IFormFile[]? FormFileList,
        string? Bio,
        int? SexualOrientationId,
        int? RelationshipGoalId,
        int[]? InterestIdList,
        int[]? LanguageIdList);
}