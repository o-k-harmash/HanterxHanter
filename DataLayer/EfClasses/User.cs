public class User
{
    public Guid UserId { get; set; }
    public DateOnly BirthDay { get; set; }
    public int Age { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte[] Avatar { get; set; }
    public string GenderId { get; set; }
    public Gender Gender { get; set; }
    public Address Address { get; set; }
}