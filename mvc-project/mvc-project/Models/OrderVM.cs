using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class OrderVM
    {

        public OrderVM(List<Order> os)
        {
            this.orders = os;
           
        }
        public List<Order> orders { get; set; }

    }
}