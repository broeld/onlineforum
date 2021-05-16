using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserProfileId { get; set; }
        public int ThreadId { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
