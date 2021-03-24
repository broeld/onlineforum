using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }

        public int UserProfileId { get; set; }
        public UserDto UserProfile { get; set; }

        public int ThreadId { get; set; }
        public ThreadDto Thread { get; set; }

        public int? RepliedPostId { get; set; }
        public PostDto RepliedPost { get; set; }

        public ICollection<NotificationDto> Notifications { get; set; }
    }
}
