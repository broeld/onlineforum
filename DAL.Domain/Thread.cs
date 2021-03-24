using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Domain
{
    public class Thread : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsOpen { get; set; }
        public DateTime ThreadOpenedDate { get; set; }
        public DateTime? ThreadClosedDate { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
