using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ChefDish.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required(ErrorMessage="Dish name is required.")]
        public string DishName { get; set; }

        [Required(ErrorMessage="Tastiness is required.")]
        [Range(1,5, ErrorMessage="Choose 1-5")]
        public int Tastiness { get; set; }

        [Required(ErrorMessage="Calories is required.")]
        [Range(0, Int32.MaxValue, ErrorMessage="Cals must be >0")]
        public int Calories { get; set; }

        [Required(ErrorMessage="Description is required.")]
        public string Desc { get; set; }


        public int ChefId { get; set; }
        // public List<Chef> Chefs { get; set; }
        public Chef Creator { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
