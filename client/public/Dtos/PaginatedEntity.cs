namespace HxH.Dtos
{
    public record PaginatedEntity<T>(long TotalCount, int Count, int Page, int Limit, List<T> EntityList);
}