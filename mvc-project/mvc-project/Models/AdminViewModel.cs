using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class AdminViewModel
    {
        public AdminViewModel(List<User> u, List<ShopItem> i, List<Order> o)
        {
            this.users = u;
            this.items = i;
            this.orders = o;
        }

        public List<User> users { get; set; }
        public List<ShopItem> items { get; set; }

        public List<Order> orders { get; set; }
    }
}