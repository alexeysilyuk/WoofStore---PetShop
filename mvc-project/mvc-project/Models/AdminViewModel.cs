using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class AdminViewModel
    {
        public AdminViewModel(List<User> u, List<ShopItem> i, List<Order> o, List<Message> m)
        {
            this.users = u;
            this.items = i;
            this.orders = o;
            this.messages = m;
        }

        public List<User> users { get; set; }
        public List<ShopItem> items { get; set; }

        public List<Order> orders { get; set; }

        public List<Message> messages { get; set; }
    }
}