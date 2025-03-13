namespace HxH.Dtos
{
    public record ProfileDtoForCreate(string Bio,
        int SexualOrientationId,
        int RelationshipGoalId,
        int[] InterestIdList,
        int[] LanguageIdList,
        IFormFile[] FormFileList);
}