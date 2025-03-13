using HxH.Dtos;
using HxH.Models;

namespace HxH.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<Geo?> GetGeoByIdAsync(int id);
        Task<Gender?> GetGenderByIdAsync(int id);
        Task<IEnumerable<TranslatedEntityDto>> GetAllGeoAsync();
        Task<IEnumerable<TranslatedEntityDto>> GetAllGenderAsync();
        Task<IEnumerable<TranslatedEntityDto>> GetAllSexualOrientationAsync();
        Task<IEnumerable<TranslatedEntityDto>> GetAllRelationshipGoalAsync();
        Task<IEnumerable<TranslatedEntityDto>> GetAllInterestsAsync();
        Task<IEnumerable<TranslatedEntityDto>> GetAllLanguagesAsync();
        Task<PaginatedEntity<User>> GetByFiltersAsync(ProfileFiltersDto filters);
        User Add(User user);
        User Update(User user);
        User Delete(User user);
        Task SaveChangesAsync();
    }
}