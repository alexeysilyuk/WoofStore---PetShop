using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mvc_project.Models
{
    public class User
    {

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

        [Key]
        [Required]
        public string username { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[a-zA-Z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$", ErrorMessage = "Password must contain at least 1 digit, 1 letter and 1 special symbol and at least 6 symbols length")]
        public string password { get; set; }

        public string ecryptedPassword { get; set; }

        [Required]
        [Phone]
        public string phone { get; set; }



    }
}