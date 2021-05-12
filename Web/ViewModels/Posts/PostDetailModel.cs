using System;

namespace Web.ViewModels
{
    public class PostDetailModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public AuthorModel UserProfile { get; set; }
        public PostReplyModel RepliedPost { get; set; }
    }
}
