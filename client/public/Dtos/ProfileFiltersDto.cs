namespace HxH.Dtos
{
    public record ProfileFiltersDto(int MaxAge, int MinAge, int Page, int Limit, int GenderId, int GeoId);
}