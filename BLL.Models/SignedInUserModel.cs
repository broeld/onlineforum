using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class SignedInUserModel
    {
        public UserModel User { get; set; }
        public string Token { get; set; }

        public SignedInUserModel(UserModel user, string token)
        {
            User = user;
            Token = token;
        }
    }
}
