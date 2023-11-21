namespace PMS.Helpers
{
    public record class PaginationMetadata(int TotalCount, int PageSize, int CurrentPage, int TotalPages, string? PreviousPageLink, string? NextPageLink);
}
