namespace Twitter.Business.Dtos.BlogDtos
{
    public class BlogDetailDto
    {
        public int Id { get; set; }
        public int UpdateCount { get; set; }
        public string Description { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public string UserId { get; set; }
       // public string UserName { get; set; }
    }
}
