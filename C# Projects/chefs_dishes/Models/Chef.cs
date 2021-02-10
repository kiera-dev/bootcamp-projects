using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ChefDish.Models
{

    public class Chef
    {
        [Key]
        public int ChefId { get; set; }

        [Required(ErrorMessage="Chef first name is required.")]
        public string ChefFirstName { get; set; }

        [Required(ErrorMessage="Chef last name is required.")]
        public string ChefLastName { get; set; }


        [Required(ErrorMessage="Chef birthday is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ChefBirthday { get; set; }


 
        public List<Dish> CreatedDishes { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
