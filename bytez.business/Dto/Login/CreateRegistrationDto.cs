﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Dto.Login
{
    public class CreateRegistrationDto
    {
        public string? Name  { get; set; }
        public string? Email  { get; set; }

        public string? Password { get; set; }    

        public  string? ConfirmPassword { get; set; }    
    }
}
