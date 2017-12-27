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
        public string FirstName { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string LastName { get; set; }


        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public int Money { get; set; }
    }
}