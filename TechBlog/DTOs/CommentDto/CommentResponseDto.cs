namespace DTOs.CommentDto
{
    public class CommentResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
