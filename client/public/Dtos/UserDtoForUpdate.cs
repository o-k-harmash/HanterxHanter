namespace HxH.Dtos
{
    public record UserDtoForUpdate(IFormFile? FormFile,
        string? FirstName,
        string? LastName,
        string? Password,
        DateOnly? BirthDay,
        string? Email,
        string? Phone,
        string? NickName,
        int? GenderId,
        int? GeoId);
}