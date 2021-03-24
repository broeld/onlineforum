using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Domain
{
    public class Notification : BaseEntity
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
