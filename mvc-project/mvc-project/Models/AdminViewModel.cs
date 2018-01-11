using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class AdminViewModel
    {
        public AdminViewModel(List<User> users, List<ShopItem> items, List<Order> orders, List<Message> messages)
        {
            this.users = users;
            this.items = items;
            this.orders = orders;
            this.messages = messages;
        }

        public List<User> users { get; set; }
        public List<ShopItem> items { get; set; }

        public List<Order> orders { get; set; }

        public List<Message> messages { get; set; }
    }
}