using HxH.Models;

namespace HxH.Dtos
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<User, IdentityDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.File))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.NickName));

            CreateMap<User, ProfileDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SexualOrientationId, opt => opt.MapFrom(src => src.Profile.SexualOrientationId))
                .ForMember(dest => dest.FileNameList, opt => opt.MapFrom(src => src.Profile.Files))
                .ForMember(dest => dest.RelationshipGoalId, opt => opt.MapFrom(src => src.Profile.RelationshipGoalId))
                .ForMember(dest => dest.LanguageIdList, opt => opt.MapFrom(src => src.Profile.Languages))
                .ForMember(dest => dest.InterestIdList, opt => opt.MapFrom(src => src.Profile.Interests))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Profile.Bio))
                .ForMember(dest => dest.GeoId, opt => opt.MapFrom(src => src.GeoId));

            CreateMap<UserDtoForCreate, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ForMember(dest => dest.Age, opt => opt.Ignore())
                .ForMember(dest => dest.HashedPassword, opt => opt.Ignore())
                .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.NickName));

            CreateMap<UserDtoForUpdate, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ForMember(dest => dest.Age, opt => opt.Ignore())
                .ForMember(dest => dest.HashedPassword, opt => opt.Ignore())
                .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.NickName));

            CreateMap<ProfileDtoForCreate, Profile>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Files, opt => opt.Ignore())
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio));

            CreateMap<string, Models.File>()
               .ForMember(dest => dest.FileName, opt => opt.AddTransform(src => src));
        }
    }
}