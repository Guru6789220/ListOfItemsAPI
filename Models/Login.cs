﻿using System.ComponentModel.DataAnnotations;

namespace ListOfItems.Models
{
    public class Login
    {
        public string? UserName { get; set; }

      //  public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }

        

    }
}
