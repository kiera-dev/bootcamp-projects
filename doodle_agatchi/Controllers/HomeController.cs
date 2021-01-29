using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Security.Cryptography;


namespace Doodle.Controllers
{
    public class HomeController : Controller
    {


        private static Random rand = new Random();
        public static string message = "";
        protected internal bool? endState = null;

        [HttpGet("")]
        public IActionResult Index()   
        {   
            
            int? happiness = HttpContext.Session.GetInt32("happiness");
            int? fullness = HttpContext.Session.GetInt32("fullness");
            int? energy = HttpContext.Session.GetInt32("energy");
            int? meals = HttpContext.Session.GetInt32("meals");   
            
            if(happiness == null && fullness == null && energy == null && meals ==null)
            {
                HttpContext.Session.SetInt32("happiness", 20);
                HttpContext.Session.SetInt32("fullness", 20);
                HttpContext.Session.SetInt32("energy", 50);
                HttpContext.Session.SetInt32("meals", 3); 
            }

            if(happiness >= 100 && fullness >= 100 && energy >= 100)
            {
                endState = true;
                message = "Yay! Your Doodlebob is all grown up and off to Doodle University! You win!! :)";
            } 

            if (happiness <= 0 || fullness <= 0)
            {
                endState = false;
                message = " You failed to properly care for your Doodlebob. It has turned into a Doodlesquid. You Lose >:(";
            }
         
            ViewBag.happiness = HttpContext.Session.GetInt32("happiness");
            ViewBag.fullness = HttpContext.Session.GetInt32("fullness");
            ViewBag.energy = HttpContext.Session.GetInt32("energy");
            ViewBag.meals = HttpContext.Session.GetInt32("meals");
            ViewBag.message = message;
            ViewBag.endState = endState;

            return View();
        }

        [HttpGet("feed")]
        public IActionResult DinnerTime()
        {
            //Feeding your Doodle costs 1 meal and gains a random amount of 
            //fullness between 5 and 10 (you cannot feed your Doodle if you do not have meals)
            int? fullness = HttpContext.Session.GetInt32("fullness");
            int? meals = HttpContext.Session.GetInt32("meals");
            if (meals <= 0)
            {
                message = "You have no food! Do some work to get some food!";
            }
            else
            {
                meals -=1;
                int chance = rand.Next(100);
                Console.WriteLine($"Meal chance: {chance}");
                    if(chance < 25) 
                    {
                        message = "Me Hah Ha Babobble. That meal was icky! Doodlebob barfed it back up! -1 meal.";
                    }
                    else
                    {
                        int mealSize = rand.Next(5,10);
                        fullness += mealSize;
                        message = $"That meal was yummy! Doodlebob wolfed it down! +{mealSize} fullness, -1 meal.";
                    }
            
            }
            HttpContext.Session.SetInt32("fullness", (int)fullness);
            HttpContext.Session.SetInt32("meals", (int)meals);
            return RedirectToAction("Index");
        }

        [HttpGet("play")]
        public IActionResult PlayTime()
        {

            int? happiness = HttpContext.Session.GetInt32("happiness");
            int? energy = HttpContext.Session.GetInt32("energy");
            if (energy <= 4)
            {
                message = "Doodlebob doesn't have enough energy! Maybe Doodlebob needs a nap...";
            }
            else
            {
                energy -=5;
                int chance = rand.Next(100);
                Console.WriteLine($"Play chance: {chance}");
                    if(chance < 25) 
                    {
                        message = "Doodlebob is bored and wants to play something else...  -5 energy.";
                    }
                    else
                    {
                        int fun = rand.Next(5,10);
                        happiness += fun;
                        message = $"Me Hoy Minoy!! Doodlebob is having so much fun! +{fun} happiness, -5 energy." ;
                    }
            HttpContext.Session.SetInt32("happiness", (int)happiness);
            HttpContext.Session.SetInt32("energy", (int)energy);            
            }
            return RedirectToAction("Index");
        }        


        [HttpGet("work")]
        public IActionResult WorkWork()
        {
            int? meals = HttpContext.Session.GetInt32("meals");
            int? energy = HttpContext.Session.GetInt32("energy");
            if (energy <= 4)
            {
                message = "Doodlebob doesn't have enough energy! Maybe Doodlebob needs a nap...";
            }
            else{
                energy -=5;
                int mealsEarned = rand.Next(1,3);
                meals += mealsEarned;
                message = $"You and Doodlebob did some work at the Krusty Krab!  +{mealsEarned} meals, -5 energy.";
            }
            HttpContext.Session.SetInt32("meals", (int)meals);
            HttpContext.Session.SetInt32("energy", (int)energy); 
            return RedirectToAction("Index");
        }



        [HttpGet("sleep")]
        public IActionResult BedTime()
        {
            int? happiness = HttpContext.Session.GetInt32("happiness");
            int? fullness = HttpContext.Session.GetInt32("fullness");
            int? energy = HttpContext.Session.GetInt32("energy");
            happiness -= 5;
            fullness -= 5;
            energy += 15;
            HttpContext.Session.SetInt32("happiness", (int)happiness);
            HttpContext.Session.SetInt32("fullness", (int)fullness);
            HttpContext.Session.SetInt32("energy", (int)energy);
            message = "Doodlebob went to bed and woke up feeling rested!";
            return RedirectToAction("Index");
        }


        [HttpGet("clear")]
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            message = "";
            return RedirectToAction("Index");
        }
        
    }
}
