using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class TopicCreateModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
