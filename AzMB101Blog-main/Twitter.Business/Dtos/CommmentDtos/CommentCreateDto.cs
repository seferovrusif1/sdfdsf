namespace Twitter.Business.Dtos.CommmentDtos
{
    public class CommentCreateDto
    {
        public string  Content { get; set; }
        public int BlogId { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
