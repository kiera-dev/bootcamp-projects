using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;

namespace Wedding.Models
{

    public class LoginUser
    {

        public string Email {get; set;}
        public string Password { get; set; }
    }
}
