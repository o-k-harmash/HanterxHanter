namespace ServiceLayer.ProfileServices
{
    public class ProfileListDto
    {
        public Guid ProfileId { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string CityString { get; set; }
        public string GenderString { get; set; }
        public string[] InterestStrings { get; set; }
        public string[] LanguageStrings { get; set; }
        public string[] FileStrings { get; set; }
    }
}
