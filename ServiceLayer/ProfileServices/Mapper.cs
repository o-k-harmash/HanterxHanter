namespace ServiceLayer.ProfileServices
{
    public class ProfileDtoToProfileProfile : AutoMapper.Profile
    {
        public ProfileDtoToProfileProfile()
        {
            CreateMap<ProfileDto, Profile>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                .ForMember(dest => dest.RelationshipGoalId, opt => opt.MapFrom(src => src.RelationshipGoalId))
                .ForMember(dest => dest.SexualOrientationId, opt => opt.MapFrom(src => src.SexualOrientationId))

                .ForMember(dest => dest.InterestLinks, opt => opt.Ignore())
                .ForMember(dest => dest.LanguageLinks, opt => opt.Ignore())

                // Маппим Files из FilesDto
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files))
                // Маппим InterestLinks из InterestsId
                .ForMember(dest => dest.InterestLinks, opt => opt.MapFrom(src => src.InterestLinks))
                // Маппим LanguageLinks из LanguagesId
                .ForMember(dest => dest.LanguageLinks, opt => opt.MapFrom(src => src.LanguageLinks));
        }
    }
}
