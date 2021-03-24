using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public PostDto Post { get; set; }
        public int UserProfileId { get; set; }
        public UserDto UserProfile { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
