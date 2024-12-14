namespace DTOs.Post
{
    public class PaginatedListDto
    {
        public List<PostDto> Posts { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
