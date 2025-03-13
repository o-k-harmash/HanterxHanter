namespace HxH.Dtos
{
    public record ProfileDto(int UserId,
        int GeoId,
        IEnumerable<string> FileNameList,
        string Name,
        int Age,
        string Bio,
        int SexualOrientationId,
        int RelationshipGoalId,
        int[] InterestIdList,
        int[] LanguageIdList);
}