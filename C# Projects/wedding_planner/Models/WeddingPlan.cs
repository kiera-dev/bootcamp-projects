using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;

namespace Wedding.Models
{

    public class WeddingPlan
    {
        [Key]
        public int WeddingId {get;set;}

        [Required(ErrorMessage="First Person required")]
        [Display(Name="First Person: ")]
        [MinLength(2, ErrorMessage="At least 2 characters required.")]
        
        public string PersonOne {get; set;}


        [Required(ErrorMessage="Second Person required")]
        [Display(Name="Second Person: ")]
        [MinLength(2, ErrorMessage="At least 2 characters required.")]
        
        public string PersonTwo { get; set; }

        [Required(ErrorMessage="Wedding Date required")]
        [Display(Name="Wedding Date: ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime WeddingDate { get; set; }


        [Display(Name="Venue Title: (optional) ")]
        public string VenueTitle { get; set; }

        [Required(ErrorMessage="Wedding Date required")]
        [Display(Name="Venue Address: ")]
        public string VenueAddress { get; set; }
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        //Connections
        public int UserId {get;set;}
        public User Creator {get;set;}
        public List<Guest> GuestList {get;set;}
        

    }
}