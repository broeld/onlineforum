using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class NotificationModel { 
        public int Id { get; set; }
        public int PostId { get; set; }
        public PostModel Post { get; set; }
        public int UserProfileId { get; set; }
        public UserModel UserProfile { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
