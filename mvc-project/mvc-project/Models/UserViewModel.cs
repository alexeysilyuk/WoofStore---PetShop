using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class UserViewModel
    {

        public UserViewModel(User user, List<User> users)
        {
            this.user = user;
            this.users = users;
        }

        public User user { get; set; }
        public List<User> users { get; set; }

    }
}