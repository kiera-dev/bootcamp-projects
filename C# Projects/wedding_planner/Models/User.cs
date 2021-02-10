
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Wedding.Models
{
    public class User
    {

        [Key]
        public int UserId {get; set;}


        [Display(Name="First Name: ")]
        [Required(ErrorMessage="First name is required.")]
        [MinLength(2, ErrorMessage="At least 2 characters required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Letters only")]
        public string FirstName { get; set; }


    
        [Display(Name="Last Name: ")]
        [Required(ErrorMessage="Last name is required.")]
        [MinLength(2, ErrorMessage="At least 2 characters required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Letters only")]
        public string LastName { get; set; }



        [Display(Name="Email: ")]
        [Required(ErrorMessage="Email address is required.")]
        // [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage="Invalid email address.")]
        public string Email { get; set; }



        [Display(Name="Password: ")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage="pw is required.")]
        [MinLength(8, ErrorMessage="At least 8 characters required.")]
        public string Password {get;set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;


        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}


        //Connections
        public List<Guest> RSVPs {get;set;}
        public List<WeddingPlan> CreatedWeddings {get;set;}
    }
}
