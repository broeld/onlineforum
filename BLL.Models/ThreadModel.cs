using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class ThreadModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsOpen { get; set; }
        public DateTime ThreadOpenedDate { get; set; }
        public DateTime? ThreadClosedDate { get; set; }

        public int UserProfileId { get; set; }
        public UserModel UserProfile { get; set; }

        public int TopicId { get; set; }
        public TopicModel Topic { get; set; }

        public ICollection<PostModel> Posts { get; set; }
    }
}
