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
        [StringLength(100, MinimumLength = 2)]
        public string fname { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string lname { get; set; }

        [Required]
        [RegularExpression("^([0-9]+)$", ErrorMessage = "Money amount must to be a number")]
        public int money { get; set; }

        [Required]
        public string photo { get; set; }

        public Boolean isAdmin { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [Phone]
        public string phone { get; set; }


    }
}