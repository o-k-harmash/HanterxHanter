namespace HxH.Models
{
    public class Profile
    {
        public int UserId { get; set; }
        public string Bio { get; private set; }
        public List<File> Files { get; set; }
        public int SexualOrientationId { get; private set; }
        public SexualOrientation SexualOrientation { get; private set; }
        public int RelationshipGoalId { get; private set; }
        public RelationshipGoal RelationshipGoal { get; private set; }
        public List<Interest> Interests { get; private set; }
        public List<Language> Languages { get; private set; }
        public List<Profile>? Meets { get; private set; }
        public List<Profile>? Likes { get; private set; }
        public int Age { get; set; }
        public int GenderId { get; set; }
        public int GeoId { get; set; }
        public Geo? Geo { get; private set; }

        public Profile() { }

        public Profile(
            int userId,
            int genderId,
            int geoId,
            string bio,
            SexualOrientation sexualOrientation,
            RelationshipGoal relationshipGoal,
            List<Interest> interests,
            List<Language> languages)
        {
            GeoId = geoId;
            GenderId = genderId;
            UserId = userId;
            Bio = bio;
            SexualOrientation = sexualOrientation;
            RelationshipGoal = relationshipGoal;
            Interests = interests;
            Languages = languages;
            Files = new List<File>();
        }
    }
}