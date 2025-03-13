namespace ServiceLayer.ProfileServices.QueryObjects
{
    public static class ProfileListDtoSelect
    {
        public static IQueryable<ProfileListDto> MapProfileToDto(this IQueryable<Profile> profiles)
        {
            return profiles.Select(profile => new ProfileListDto
            {
                ProfileId = profile.UserId,  // Здесь присваиваем UserId профиля в ProfileId DTO
                Age = profile.Age,           // Преобразуем Age
                Name = profile.Name,         // Преобразуем Name
                CityString = profile.CityId, // Преобразуем CityId в строку (если CityId — это какой-то ID города, то лучше получить имя города)
                GenderString = profile.GenderId, // Преобразуем GenderId в строку
                Bio = profile.Bio,
                InterestStrings = profile.InterestLinks
                    .Select(link => link.Interest.InterestId)
                    .ToArray(), // Преобразуем интересы
                LanguageStrings = profile.LanguageLinks
                    .Select(link => link.Language.LanguageId)
                    .ToArray(), // Преобразуем языки
                FileStrings = profile.Files
                    .Select(file => file.FileId)
                    .ToArray()  // Преобразуем файлы
            });
        }
    }
}