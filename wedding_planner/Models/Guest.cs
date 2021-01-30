using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;

namespace Wedding.Models
{

    public class Guest 
    //this is the middle table, right? 
    //I always have trouble figuring out what should be the middle table...
    //it seems a little easier in MySql vs yucky Django.
    {
        [Key]
        public int GuestId {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;


        
        //Connections
        public int UserId {get;set;}
        public User User {get;set;}        
        public int WeddingId {get;set;}
        public WeddingPlan WeddingPlan {get;set;}


        
    }
}