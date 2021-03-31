using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }

        public int UserProfileId { get; set; }
        public UserModel UserProfile { get; set; }

        public int ThreadId { get; set; }
        public ThreadModel Thread { get; set; }

        public int? RepliedPostId { get; set; }
        public PostModel RepliedPost { get; set; }

        public ICollection<NotificationModel> Notifications { get; set; }
    }
}
