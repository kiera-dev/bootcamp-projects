using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Login.Models;
using Microsoft.AspNetCore.Identity;


namespace Login.Controllers
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
            return Redirect("login");
        }



        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("Login");
        }


        [HttpGet("register")]
        public IActionResult Register()
        {
            return View("register");
        }


        [HttpPost("log")]
        public IActionResult LoginPost(LoginUser usersub)
        {
            if (ModelState.IsValid)
            { 
                User userInDb = _context.Users.FirstOrDefault(u => u.Email == usersub.Email);
                if (userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email");
                    return View("Login");
                }

                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                PasswordVerificationResult result = hasher.VerifyHashedPassword(usersub, userInDb.Password, usersub.Password);
                if (result == 0)
                {   
                    ModelState.AddModelError("Password", "This password is incorrect");
                    return View("Login");
                }
                else
                {
                    HttpContext.Session.SetInt32("currentUser", userInDb.UserId);
                    return RedirectToAction("Success");
                }
            }
            System.Console.WriteLine("omg Model State is NOT Valid!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            return RedirectToAction("Login");
            }


        [HttpPost("regUser")]
        public IActionResult CreateUser(User FromForm)
        {
            if(ModelState.IsValid)
            {
                
                if(_context.Users.Any(u => u.Email == FromForm.Email))
                {           
                    ModelState.AddModelError("Email", "Email already in use!");
                    Console.WriteLine("debug: email in use!!!!!");
                    return View("Register");    
                }

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                FromForm.Password = Hasher.HashPassword(FromForm, FromForm.Password);
                _context.Add(FromForm);
                _context.SaveChanges();
                 
                var user = _context.Users.FirstOrDefault(u => u.Email == FromForm.Email);
                int currentUser = user.UserId;
                HttpContext.Session.SetInt32("currentUser", currentUser);
                return RedirectToAction("Success");
            } 
            else
            {
                Console.WriteLine("debug: omg Model State is NOT Valid!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                return View("Register");     
            }
        }
    

        [HttpGet("success")]
        public IActionResult Success()
        {
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            if (currentUserId == null)
                {
                    return RedirectToAction("Index");
                }
            return View();
        }

        [HttpGet("clear")]
            public IActionResult Logout()
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index");
            }

    }
}
