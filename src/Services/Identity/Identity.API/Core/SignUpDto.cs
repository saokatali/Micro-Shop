﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.Core
{
    public class SignUpDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public IList<String> Roles { get; set; }

    }
}
