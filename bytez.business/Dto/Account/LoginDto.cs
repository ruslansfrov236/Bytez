﻿using System;
using System.ComponentModel.DataAnnotations;

namespace bytez.business.Dto.Login
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Url)]
        public string? ReturnUrl { get; set; }
    }
}
