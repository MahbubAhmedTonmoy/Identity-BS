﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UAM.DTO
{
    public class UserRegistrationDTO
    {
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        //[EmailValidation(allowedDomain: "yopmail.com", ErrorMessage = "Email domain must be yopmail.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Password")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password",
        ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public List<string> Role { get; set; }
        public bool TwoFactorEnabled { get; set; } = false;
    }
}
