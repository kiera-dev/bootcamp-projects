using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Crud.Models;


namespace Crud.Controllers
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
            List<Dish> LastFiveDishes = _context.Dishes.OrderByDescending(o => o.CreatedAt).Take(5).ToList();
            return View(LastFiveDishes);
        }


        [HttpGet("{DishId}")]
        public IActionResult Info(int DishId)
        {
            Dish dishInfo = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);
            return View(dishInfo);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult CreateDish(Dish FromForm)
        {
            _context.Add(FromForm);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet("edit/{DishId}")]
        public IActionResult Edit(int DishId)
        {
            Dish dishToEdit = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);
            return View(dishToEdit);
        }

        [HttpPost("update/{DishId}")]
        public IActionResult EditDish(int DishId, Dish FromForm)
        {
            Dish Retrieved = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);
            if(Retrieved == null) 
            {
                return RedirectToAction("Index");
            }
                FromForm.DishId = DishId;
                Retrieved.Name = FromForm.Name;
                Retrieved.Chef= FromForm.Chef;
                Retrieved.Calories= FromForm.Calories;
                Retrieved.Tastiness = FromForm.Tastiness;
                Retrieved.UpdatedAt = FromForm.UpdatedAt;
                _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{DishId}")]
        public IActionResult DeleteDish(int DishId)
        {

            Dish ToDelete = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);

            if(ToDelete == null)
            {
                return RedirectToAction("Index");
            }
            _context.Remove(ToDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
 }
