using System;

namespace Web.ViewModels
{
    public class NotificationCreateModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserProfileId { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
