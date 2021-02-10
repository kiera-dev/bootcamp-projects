using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Survey.Models;

namespace Survey.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Redirect("blankform");
        }


        [HttpGet("blankform")]
        public IActionResult Form()
        {
            // make query to database
            // store data in viewbag and render it on the page
            return View("form");
        }

        [HttpPost("FormInfo")]
        public IActionResult CreateData(FormData FromForm)
        {
            if(ModelState.IsValid)
            {

                System.Console.WriteLine(FromForm.PersonName);
                return View("FormInfo", FromForm);
            }
            else 
            {
                return Form();
            }
        }


    }
}
