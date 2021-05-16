using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; } = false;

        public ICollection<PostModel> Posts { get; set; }
        public ICollection<ThreadModel> Threads { get; set; }
        public ICollection<NotificationModel> Notifications { get; set; }
    }
}
