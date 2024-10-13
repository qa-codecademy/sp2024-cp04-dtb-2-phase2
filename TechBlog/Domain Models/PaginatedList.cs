namespace Domain_Models
{
    public class PaginatedList
    {
        public List<Post> Posts { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; }
        public bool HasNextPage => PageIndex < TotalPages;
        public PaginatedList(List<Post> posts, int pageIndex, int totalPages)
        {
            Posts = posts; 
            PageIndex = pageIndex;
            TotalPages = totalPages;
        }
    }
}