namespace HxH.Models
{
    public class User
    {
        public int Id { get; private set; }

        public User() { }

        public string Email { get; private set; }
        public byte[] Avatar { get; set; }
        public string PhoneNumber { get; private set; }
        public bool IsPhoneNumberApproved { get; private set; }
        public string? HashedPassword { get; set; }
        public bool IsEmailApproved { get; private set; }
        public string NickName { get; private set; }
        public DateOnly BirthDay { get; private set; }
        public int? ProfileId { get; private set; }
        public Profile? Profile { get; set; }
        public int Age { get; set; }
        public int GeoId { get; private set; }
        public Geo Geo { get; private set; }
        public int GenderId { get; private set; }
        public Gender Gender { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public User(
           string email,
           string phoneNumber,
           DateOnly birthDay,
           int age,
           string firstName,
           string lastName,
           string nickName,
           Geo geo,
           Gender gender)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            BirthDay = birthDay;
            Age = age;
            Geo = geo;
            Gender = gender;
            FirstName = firstName;
            LastName = lastName;
            NickName = nickName;
            IsEmailApproved = false;
            IsPhoneNumberApproved = false;
        }
    }
}