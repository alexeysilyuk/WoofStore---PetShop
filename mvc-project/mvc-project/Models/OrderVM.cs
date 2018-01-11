using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class OrderVM
    {

        public OrderVM(List<Order> orders)
        {
            this.orders = orders;
           
        }
        public List<Order> orders { get; set; }

    }
}