namespace Domain_Models
{
    public class PaginatedList
    {
        public List<Post> Posts { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}