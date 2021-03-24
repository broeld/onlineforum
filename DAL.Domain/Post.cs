using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Domain
{
    public class Post : BaseEntity
    {
        public string Content { get; set; }
        public DateTime PostDate { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public int ThreadId { get; set; }
        public Thread Thread { get; set; }

        public int? RepliedPostId { get; set; }
        public Post RepliedPost { get; set; }

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Post> Replies { get; set; } = new List<Post>();
    }
}
