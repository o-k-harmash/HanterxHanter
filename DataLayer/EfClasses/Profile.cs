using System.ComponentModel.DataAnnotations;

public class Profile
{
    // Приватные поля
    private Guid _userId;
    private string _name;
    private string _genderId;
    private int _age;
    private string _cityId;
    private string _sexualOrientationId;
    private SexualOrientation _sexualOrientation;
    private string _relationshipGoalId;
    private RelationshipGoal _relationshipGoal;
    private ICollection<ProfileInterest> _interestLinks;
    private ICollection<ProfileLanguage> _languageLinks;
    private ICollection<File> _files;

    // Публичный геттер и сеттер для ProfileId
    [Key]
    [Required]
    public Guid UserId
    {
        get => _userId;
        set
        {
            if (value == Guid.Empty)
                throw new ArgumentException("ProfileId cannot be an empty Guid.");
            _userId = value;
        }
    }

    // Публичный геттер и сеттер для GenderId
    public string GenderId
    {
        get => _genderId;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("GenderId cannot be null or empty.");
            _genderId = value;
        }
    }

    // Публичный геттер и сеттер для Age
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("CityId cannot be null or empty.");
            _name = value;
        }
    }

    // Публичный геттер и сеттер для Age
    public int Age
    {
        get => _age;
        set
        {
            if (value < 0)
                throw new ArgumentException("Age cannot be less then zero.");
            _age = value;
        }
    }

    // Публичный геттер и сеттер для CityId
    public string CityId
    {
        get => _cityId;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("CityId cannot be null or empty.");
            _cityId = value;
        }
    }

    // Публичный геттер и сеттер для SexualOrientationId
    public string SexualOrientationId
    {
        get => _sexualOrientationId;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("SexualOrientationId cannot be null or empty.");
            _sexualOrientationId = value;
        }
    }

    // Публичный геттер и сеттер для SexualOrientation
    public SexualOrientation SexualOrientation
    {
        get => _sexualOrientation;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(SexualOrientation), "SexualOrientation cannot be null.");
            _sexualOrientation = value;
        }
    }

    // Публичный геттер и сеттер для RelationshipGoalId
    public string RelationshipGoalId
    {
        get => _relationshipGoalId;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("RelationshipGoalId cannot be null or empty.");
            _relationshipGoalId = value;
        }
    }

    // Публичный геттер и сеттер для RelationshipGoal
    public RelationshipGoal RelationshipGoal
    {
        get => _relationshipGoal;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(RelationshipGoal), "RelationshipGoal cannot be null.");
            _relationshipGoal = value;
        }
    }

    // Публичный геттер и сеттер для InterestLinks
    public ICollection<ProfileInterest> InterestLinks
    {
        get => _interestLinks;
        set
        {
            if (value == null || value.Count == 0)
                throw new ArgumentException("InterestLinks cannot be null or empty.");
            _interestLinks = value;
        }
    }

    // Публичный геттер и сеттер для LanguageLinks
    public ICollection<ProfileLanguage> LanguageLinks
    {
        get => _languageLinks;
        set
        {
            if (value == null || value.Count == 0)
                throw new ArgumentException("LanguageLinks cannot be null or empty.");
            _languageLinks = value;
        }
    }

    // Публичный геттер и сеттер для Files
    public ICollection<File> Files
    {
        get => _files;
        set
        {
            if (value == null || value.Count == 0)
                throw new ArgumentException("Files cannot be null or empty.");
            _files = value;
        }
    }
}
