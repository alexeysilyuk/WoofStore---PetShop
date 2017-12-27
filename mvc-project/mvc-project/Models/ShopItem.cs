using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mvc_project.Models
{
    public class ShopItem
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string Description { get; set; }


        [Key]
        [Required]
        public int price { get; set; }

        [Required]
        public int photo_url { get; set; }
    }
}