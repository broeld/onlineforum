using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class PostCreateModel
    {
        [Required]
        public string Content { get; set; }
        public int UserProfileId { get; set; }
        public int ThreadId { get; set; }
        public int? RepliedPostId { get; set; }

    }
}
