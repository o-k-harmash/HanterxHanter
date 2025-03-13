namespace ServiceLayer.UserServices
{
    public class UserDtoToUserUser : AutoMapper.Profile
    {
        public UserDtoToUserUser()
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))

                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.GenderId))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            CreateMap<User, Profile>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))

                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.GenderId))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.Address.CityId));
        }
    }
}
