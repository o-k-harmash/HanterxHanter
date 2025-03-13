using HxH.App.Models;
using HxH.Dtos;

namespace HxH.Services
{
    public interface IUserService
    {
        Task<Result<IdentityDto>> GetUserByIdAsync(int id);
        Task<Result<IEnumerable<ProfileDto>>> GetProfileByFiltersAsync(ProfileFiltersDto filters);
        Task<Result<IdentityDto>> AddUser(UserDtoForCreate user);
        Task<Result<ProfileDto>> AddProfile(int id, ProfileDtoForCreate user);
        Task<Result<ProfileDto>> UpdateProfile(int id, ProfileDtoForUpdate user);
        Task<Result<IdentityDto>> UpdateUser(int userId, UserDtoForUpdate user);
        Task<Result<IdentityDto>> DeleteUser(int userId);
    }
}