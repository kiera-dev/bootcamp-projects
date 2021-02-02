using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using ChefDish.Models;


namespace ChefDish.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }
     
        [HttpGet("")]
        public IActionResult Index()
        {
            List<Chef> ChefRoster = _context.Chefs.Include(d => d.CreatedDishes).ToList();
            return View(ChefRoster);
        }


        [HttpGet("dishes")]
        public IActionResult Dishes()
        {
            List<Dish> DishList = _context.Dishes.Include(d => d.Creator).ToList();
            // ViewBag.DishList = DishList;
            return View(DishList);
        }


        [HttpGet("newdish")]
        public IActionResult NewDish()
        {
            List<Chef> ChefList = _context.Chefs.ToList();
            ViewBag.ChefList = ChefList;

            return View();
        }

        [HttpPost("createD")]
        public IActionResult CreateDish(Dish FromForm)
        {
            if (ModelState.IsValid)
            { 
                _context.Add(FromForm);
                _context.SaveChanges();
                return RedirectToAction("Dishes");
            }
            else
            {
                List<Chef> ChefList = _context.Chefs.ToList();
                ViewBag.ChefList = ChefList;
                return View("newdish");
            }
        }

        [HttpGet("newchef")]
        public IActionResult NewChef()
        {
            return View();
        }


        [HttpPost("createC")]
        public IActionResult CreateChef(Chef FromForm)
        {
            if (ModelState.IsValid)
            { 
                // if(FromForm.ChefBirthday >= DateTime.Now)
                // {
                //     ModelState.AddModelError("Birthday", "Birthday must be in the past...");
                //     return View("newchef");
                // } //oh..I guess you can't be 18 if birthday is in the future so...this takes care of both cases..
                DateTime now = DateTime.Now;
                DateTime banana = FromForm.ChefBirthday;
                int age = now.Year - banana.Year;
                if(age < 18)
                {
                    ModelState.AddModelError("Birthday", "Chef must be 18+");
                    return View("newchef");
                }
                _context.Add(FromForm);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("newchef");
            }
        }
    }
}
