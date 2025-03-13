using HxH.App.Models;
using HxH.Dtos;
using HxH.Infrastructure;
using HxH.Interfaces;
using HxH.Models;

namespace HxH.Services
{
    public class ProfileService
    {
        private List<Models.File> _uploadedFileList = new List<Models.File>();
        private List<Models.File> _fileToRemoveList = new List<Models.File>();
        private readonly IUserRepository _userRepo;
        private readonly IProfileRepository _profileRepo;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IPictureService _pictureService;

        public ProfileService(IProfileRepository profileRepo, IUserRepository userRepo, AutoMapper.IMapper mapper, IPictureService pictureService)
        {
            _profileRepo = profileRepo;
            _userRepo = userRepo;
            _mapper = mapper;
            _pictureService = pictureService;
        }

        public async Task<Result<IEnumerable<ProfileDto>>> GetProfileByFiltersAsync(ProfileFiltersDto profileFilters)
        {
            var result = new Result<IEnumerable<ProfileDto>>();

            try
            {
                var paginatedProfileList = await _profileRepo.GetProfileListAsync(profileFilters.Page,
                    profileFilters.Limit,
                    profileFilters.MinAge,
                    profileFilters.MaxAge,
                    profileFilters.GeoId,
                    profileFilters.GenderId);

                result.Value = paginatedProfileList.EntityList.Select(profile => _mapper.Map<ProfileDto>(profile));

                return result;
            }
            catch (Exception)
            {
                return result += new Exception();
            }
        }

        public async Task<Result<ProfileDto>> CreateProfileAsync(int userId, ProfileDtoForCreate profileDto)
        {
            var result = new Result<ProfileDto>();

            try
            {
                var user = await _userRepo.GetByIdAsync(userId);

                ArgumentNullException.ThrowIfNull(user);

                var profile = _mapper.Map<Profile>(profileDto);

                profile.GenderId = user.GenderId;
                profile.UserId = user.Id;
                profile.GeoId = user.GeoId;
                profile.Age = user.Age;

                foreach (var formFile in profileDto.FormFileList)
                {
                    _uploadedFileList.Add(CreateFileOrThrow(formFile));
                }

                profile.Files = _uploadedFileList;

                _profileRepo.Create(profile);

                await _profileRepo.SaveChangesAsync();

                return result += _mapper.Map<ProfileDto>(profile);
            }
            catch (Exception)
            {
                TryToRemoveFileList(_uploadedFileList);

                return result += new Exception();
            }
        }

        public async Task<Result<ProfileDto>> UpdateProfileAsync(int userId, ProfileDtoForUpdate profileDto)
        {
            var result = new Result<ProfileDto>();

            var profile = await _profileRepo.GetAsync(userId);

            try
            {
                ArgumentNullException.ThrowIfNull(profile);

                profile = _mapper.Map(profileDto, profile);

                if (profileDto.FormFileList != null)
                {
                    _fileToRemoveList = profile.Files;

                    foreach (var formFile in profileDto.FormFileList)
                    {
                        _uploadedFileList.Add(CreateFileOrThrow(formFile));
                    }

                    profile.Files = _uploadedFileList;
                }

                _profileRepo.Update(profile);

                await _profileRepo.SaveChangesAsync();

                result += _mapper.Map<ProfileDto>(profile);

                TryToRemoveFileList(_fileToRemoveList);

                return result;
            }
            catch (Exception)
            {
                TryToRemoveFileList(_uploadedFileList);

                return result += new Exception();
            }
        }

        private Models.File CreateFileOrThrow(IFormFile formFile)
        {
            var fileResult = _pictureService.UploadFile(formFile);

            if (fileResult.IsFail)
                throw new ArgumentException();

            return new Models.File { FileName = fileResult.Value };
        }

        private void TryToRemoveFileList(List<Models.File> fileList)
        {
            foreach (var file in fileList)
            {
                _pictureService.RemoveFile(file.FileName);
            }
        }
    }
}