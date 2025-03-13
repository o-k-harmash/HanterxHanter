namespace HxH.Dtos
{
    public record UserDtoForCreate(string FirstName,
        string LastName,
        string Password,
        DateOnly BirthDay,
        string Email,
        string Phone,
        string NickName,
        int GenderId,
        int GeoId,
        IFormFile FormFile);
}