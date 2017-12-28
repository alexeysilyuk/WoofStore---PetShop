using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class ShopItemViewModel
    {

        public ShopItemViewModel(ShopItem i, List<ShopItem> items)
        {
            this.i = i;
            this.items = items;
        }

        public ShopItem i { get; set; }
        public List<ShopItem> items { get; set; }

    }
}