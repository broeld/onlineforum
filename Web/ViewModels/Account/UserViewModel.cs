using System;

namespace Web.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
