using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mvc_project.Models
{
    public class User
    {

        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string fname { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string lname { get; set; }

        [Required]
        [RegularExpression("^([0-9]+)$", ErrorMessage = "Price has to be number")]
        public int price { get; set; }

        [Required]
        public string photo { get; set; }

        [Required]
        public Boolean isAdmin { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [Phone]
        public string phone { get; set; }


    }
}