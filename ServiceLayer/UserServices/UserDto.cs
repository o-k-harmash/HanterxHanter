using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ServiceLayer.ProfileServices;

public class UserDto : IDisposable
{
    private bool _disposed = false;  // To track if Dispose has been called

    // Private backing fields for calculated properties
    private int _age;
    private byte[] _avatar;

    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDay { get; set; }
    public string GenderId { get; set; }
    [JsonIgnore]
    public string CountryId { get; set; }
    [JsonIgnore]
    public string StateId { get; set; }
    [JsonIgnore]
    public string CityId { get; set; }

    [JsonIgnore]
    public IFormFile FormFile { get; set; }

    [JsonIgnore]
    [ValidateNever]
    public FileDto FileDto { get; set; }

    [ValidateNever]
    public Address Address { get; set; }

    // Getter and Setter for Age - calculated field
    public int Age
    {
        get
        {
            // Calculate the age dynamically from the BirthDay
            var dateNow = DateTime.Now;
            return dateNow.Year - BirthDay.Year - (dateNow.Month < BirthDay.Month || (dateNow.Month == BirthDay.Month && dateNow.Day < BirthDay.Day) ? 1 : 0);
        }
        private set => _age = value; // Only internal setter for consistency
    }

    // Getter and Setter for Avatar - calculated field from FileDto
    [ValidateNever]
    public byte[] Avatar
    {
        get => _avatar;
        private set => _avatar = value; // Only internal setter for consistency
    }

    public void BeforeExecute()
    {
        UserId = UserId == Guid.Empty
            ? Guid.NewGuid()
            : UserId;

        Console.WriteLine(UserId);

        var fileDto = new FileDto
        {
            FormFile = FormFile
        };

        fileDto.BeforeExecute(true);
        FileDto = fileDto;

        Avatar = FileDto.FileBuffer;

        Address = new Address
        {
            UserId = UserId,
            CountryId = CountryId,
            StateId = StateId,
            CityId = CityId
        };
    }

    // Implement Dispose pattern
    public void Dispose()
    {
        // Dispose managed resources
        Dispose(true);

        // Suppress finalization to prevent the finalizer from being called.
        GC.SuppressFinalize(this);
    }

    // Core Dispose method, releases resources
    protected virtual void Dispose(bool disposing)
    {
        // Check if the object has already been disposed
        if (_disposed)
            return;

        // If disposing is true, we are cleaning up managed resources
        if (disposing)
        {
            // Dispose of managed resources (i.e. FormFile and FileDto)
            if (FileDto != null)
            {
                FileDto.Dispose();
                FileDto = null;
            }

            if (FormFile != null)
            {
                FormFile = null;
            }

            if (Address != null)
            {
                Address = null;
            }

            if (_avatar != null)
            {
                Array.Clear(_avatar, 0, _avatar.Length);  // Clear the avatar byte array
                _avatar = null;
            }
        }

        // Mark the object as disposed
        _disposed = true;
    }

    // Finalizer (called only if Dispose is not explicitly called)
    ~UserDto()
    {
        Dispose(false);
    }
}
