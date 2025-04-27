namespace IceCreamService.Application.DTOs
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<T>? Data { get; set; }
    }
}
