using System.Security.Cryptography;
using System.Text;
using HxH.App.Models;
using HxH.Dtos;
using HxH.Infrastructure;
using HxH.Models;


namespace HxH.Services
{
    public class UserService
    {
        private readonly IUrepository _userRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IPictureService _pictureService;

        public UserService(IUrepository userRepository, AutoMapper.IMapper mapper, IPictureService pictureService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _pictureService = pictureService;
        }

        public async Task<Result<IdentityDto>> GetUserByIdAsync(int userId)
        {
            var result = new Result<IdentityDto>();

            try
            {
                var user = await GetUserOrThrow(userId);

                return result += _mapper.Map<IdentityDto>(user);
            }
            catch (Exception)
            {
                return result += new Exception();
            }
        }

        public async Task<Result<IdentityDto>> CreateUser(UserDtoForCreate userDto)
        {
            var result = new Result<IdentityDto>();

            try
            {
                var user = _mapper.Map<User>(userDto);

                user.Age = AgeFromBithDayCalculator(userDto.BirthDay);

                user.HashedPassword = SimplePasswordSha256Hasher(userDto.Password);

                var fileResult = _pictureService.ReadFile(userDto.FormFile);

                if (fileResult.IsFail)
                    throw new ArgumentException("Something wrong with loading file.", nameof(UserDtoForCreate));

                user.Avatar = fileResult.Value;

                _userRepository.Create(user);

                await _userRepository.SaveChangesAsync();

                return result += _mapper.Map<IdentityDto>(user);
            }
            catch (Exception)
            {
                return result += new Exception();
            }
        }

        public async Task<Result<IdentityDto>> UpdateUser(int userId, UserDtoForUpdate userDto)
        {
            var result = new Result<IdentityDto>();

            try
            {
                var user = await GetUserOrThrow(userId);

                user = _mapper.Map(userDto, user);

                if (userDto.BirthDay.HasValue)
                    user.Age = AgeFromBithDayCalculator(userDto.BirthDay.Value);

                if (userDto.Password is not null)
                    user.HashedPassword = SimplePasswordSha256Hasher(userDto.Password);

                if (userDto.FormFile is not null)
                {
                    var fileResult = _pictureService.ReadFile(userDto.FormFile);

                    if (fileResult.IsFail)
                        throw new ArgumentException("Something wrong with loading file.", nameof(UserDtoForCreate));

                    user.Avatar = fileResult.Value;
                }

                _userRepository.Update(user);

                await _userRepository.SaveChangesAsync();

                return result += _mapper.Map<IdentityDto>(user);
            }
            catch (Exception)
            {
                return result += new Exception();
            }
        }

        public async Task<Result<IdentityDto>> DeleteUser(int userId)
        {
            var result = new Result<IdentityDto>();

            try
            {
                var user = await GetUserOrThrow(userId);

                _userRepository.Delete(user);

                await _userRepository.SaveChangesAsync();

                return result += _mapper.Map<IdentityDto>(user);
            }
            catch (Exception)
            {
                return result += new Exception();
            }
        }

        private async Task<User> GetUserOrThrow(int userId)
        {
            var user = await _userRepository.GetAsync(userId);

            ArgumentNullException.ThrowIfNull(user);

            return user;
        }

        private int AgeFromBithDayCalculator(DateOnly birthDay)
        {
            var today = DateTime.Today;
            return today.Year - birthDay.Year;
        }

        private string SimplePasswordSha256Hasher(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}