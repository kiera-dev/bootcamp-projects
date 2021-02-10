using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Bank.Models
{

    public class Transaction
    {
        [Key]
        public int TransactionId {get; set;}

        [Required(ErrorMessage = "You need to put in an Amount...")]
        [Range(0, Int32.MaxValue)] 
        public decimal Amount {get; set;}
        public string Type {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        public int UserId {get; set;}
        public User User { get; set; }
        
        
    }
}
