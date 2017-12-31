using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class Order
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public String username { get; set; }

        [Required]
        public int[] items { get; set; }

        [Required]
        public int totalSum { get; set; }

        [Required]
        public int status { get; set; }
    }
}