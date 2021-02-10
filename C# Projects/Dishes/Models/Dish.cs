using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace Crud.Models
{
    public class Dish
    {
        [Key]
        public int DishId {get;set;}

        [Required(ErrorMessage="Name is required.")]
        public string Name {get;set;}

        [Required(ErrorMessage="Chef is required.")]
        public string Chef {get;set;}

        [Required(ErrorMessage="Tastiness is required.")]
        [Range(1,5, ErrorMessage="Choose 1-5")]
        public int Tastiness {get;set;}

        [Required(ErrorMessage="Calories is required.")]
        [Range(0, Int32.MaxValue, ErrorMessage="Cals must be >0")]
        public int Calories {get;set;}

        [Required(ErrorMessage="Description is required.")]
        public string Desc {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}