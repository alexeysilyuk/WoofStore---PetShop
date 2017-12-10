using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class UserViewModel
    {

        public UserViewModel(User u, List<User> us)
        {
            this.user = u;
            this.users = us;
        }

        public User user { get; set; }
        public List<User> users { get; set; }

    }
}