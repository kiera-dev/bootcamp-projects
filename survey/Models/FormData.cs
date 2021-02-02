
using System;
using System.ComponentModel.DataAnnotations;

namespace Survey.Models
{
    public class FormData
    {
        [Display(Name=" Name: ", Prompt="enter name here")]
        [Required(ErrorMessage="Name is required.")]
        [MinLength(2, ErrorMessage="Too short.")]
        public string PersonName { get; set; }
    
        [Display(Name="Location: ")]
        [Required(ErrorMessage="Location is required.")]
        public string Location { get; set; }

        [Display(Name="Favorite: ")]
        [Required(ErrorMessage="Selection is required.")]
     
        public string Favorite { get; set; }

        [Display(Name="Comment: ")]
        [MaxLength(20, ErrorMessage="That's too long.")]
        public string Comment { get; set; }

    }
}