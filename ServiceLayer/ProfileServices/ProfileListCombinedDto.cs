namespace ServiceLayer.ProfileServices
{
    public class ProfileListCombinedDto
    {
        public FilterPageOptions FilterPageData { get; set; }
        public IEnumerable<ProfileListDto> ProfileList { get; set; }
    }
}
