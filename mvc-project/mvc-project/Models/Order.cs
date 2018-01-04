using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_project.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderID { get; set; }

        [Required]
        public String username { get; set; }

        [Required]
        public int itemID { get; set; }

        [Required]
        public string status { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string img { get; set; }

        [Required]
        public int price { get; set; }
    }
}