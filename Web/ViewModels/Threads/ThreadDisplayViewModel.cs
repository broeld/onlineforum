using System;

namespace Web.ViewModels
{
    public class ThreadDisplayViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsOpen { get; set; }
        public DateTime ThreadOpenedDate { get; set; }
        public int PostsNumber { get; set; }
        public AuthorModel UserProfile { get; set; }
    }
}
