namespace ServiceLayer.ProfileServices.QueryObjects
{
    public static class BookListDtoFilter
    {
        public static IQueryable<Profile> FilterProfilesBy(
            this IQueryable<Profile> profiles,
            string cityString, string genderString, int maxAge, int minAge)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(cityString);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(genderString);

            return profiles.Where(x => x.GenderId == genderString
                && x.CityId == cityString
                && x.Age >= minAge
                && x.Age <= maxAge);
        }
    }
}