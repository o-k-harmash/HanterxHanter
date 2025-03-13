namespace HxH.Dtos
{
    public record IdentityDto(int Id,
        string FileName,
        string FirstName,
        string LastName,
        DateOnly BirthDay,
        string Email,
        string Phone,
        string NickName,
        int GenderId,
        int GeoId);
}