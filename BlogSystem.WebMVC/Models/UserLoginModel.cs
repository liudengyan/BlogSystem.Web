using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.WebMVC.Models
{
    public class UserLoginModel
    {
        [Required]
        [EmailAddress]
        public string  Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}