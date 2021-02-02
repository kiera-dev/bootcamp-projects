using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using ModelFun.Models;

namespace ModelFun
{
    public class HomeController : Controller
    {
        [Route("")]  
        [HttpGet]   
        public IActionResult Index()
        {
            string MyMessage = "Halvah sweet roll chupa chups gummies. Cookie soufflé biscuit sugar plum bear claw soufflé pie cookie. Wafer oat cake soufflé brownie. Jelly gingerbread ice cream oat cake. Tootsie roll gummies chocolate cake.";
            
            return View("index", MyMessage);
        }



        [Route("numbers")]  
        [HttpGet]   
        public IActionResult Numbers()
        {
            int[] MyNumbers = { 1,2,3,4,5 } ;
            return View("numbers", MyNumbers);
        }


        [Route("user")]  
        [HttpGet]   
        public IActionResult Person()
        {
            User MyUser = new User()
            {
                FirstName =  "Freddy",
                LastName = "Kreuger",
            };
            return View("user", MyUser);
        }


        [Route("users")]  
        [HttpGet]   
        public IActionResult Users()
        {
            List<User> AllUsers = new List<User>();
            AllUsers.Add(new User(){FirstName = "Ash", LastName = "Williams"});
            AllUsers.Add(new User(){FirstName = "Jason", LastName = "Vorhees"});
            AllUsers.Add(new User(){FirstName = "Michael", LastName = "Myers"});

            
            return View("users", AllUsers);
        }
    }
}
