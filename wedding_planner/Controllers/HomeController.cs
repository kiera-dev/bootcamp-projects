using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Wedding.Models;
using Microsoft.AspNetCore.Identity;


namespace Wedding.Controllers //UGH GOD why didn't I name the project WeddingPlanner T_T
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
                    HttpContext.Session.Clear();
                    HttpContext.Session.SetInt32("currentUser", userInDb.UserId);
                    return RedirectToAction("Dashboard");
                }
            }
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
                    return View("Login");    
                }

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                FromForm.Password = Hasher.HashPassword(FromForm, FromForm.Password);
                _context.Add(FromForm);
                _context.SaveChanges();
                 
                var user = _context.Users.FirstOrDefault(u => u.Email == FromForm.Email);
                int currentUser = user.UserId;
                HttpContext.Session.Clear();
                HttpContext.Session.SetInt32("currentUser", currentUser);
                return RedirectToAction("Dashboard");
            } 
            else
            {
                return View("Login");     
            }
        }
    



        [HttpGet("dashboard")]
        public IActionResult Dashboard(int weddingId)
        {
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            if (currentUserId == null)
                {
                    return RedirectToAction("Index");
                }
            User currentUser = _context.Users.Include(c => c.CreatedWeddings).FirstOrDefault(u => u.UserId == (int)currentUserId);
            ViewBag.currentUser = currentUser;
            // List<WeddingPlan> weddingList = _context.WeddingPlans.Include(g => g.GuestList).ToList();
            List<WeddingPlan> weddingList = _context.WeddingPlans.Include(g => g.GuestList).ThenInclude(u => u.User).ToList();
            return View(weddingList);
        }



        [HttpGet("delwedding/{weddingId}")]
        public IActionResult DeleteWedding(int weddingId)
        {
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            if (currentUserId == null)
                {
                    return RedirectToAction("Index");
                }
            WeddingPlan deleteWedding = _context.WeddingPlans.FirstOrDefault(p => p.WeddingId == weddingId);
            _context.WeddingPlans.Remove(deleteWedding);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
                



        
        [HttpGet("delguest/{weddingId}")]
        public IActionResult DeleteGuest(int weddingId)
        {
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            if (currentUserId == null)
                {
                    return RedirectToAction("Index");
                }
            Guest deleteGuest = _context.Guests.Where(g => g.WeddingId == weddingId).FirstOrDefault(u => u.UserId == (int)currentUserId);
            _context.Guests.Remove(deleteGuest);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        
        


        [HttpGet("addguest/{weddingId}")]
        public IActionResult AddGuest(int weddingId)
        {
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            if (currentUserId == null)
                {
                    return RedirectToAction("Index");
                }
            User currentUser = _context.Users.FirstOrDefault(u => u.UserId == (int)currentUserId);
            WeddingPlan wedding = _context.WeddingPlans.FirstOrDefault(w => w.WeddingId == weddingId);
            Guest addGuest = new Guest();
                addGuest.UserId = (int)currentUserId;
                addGuest.WeddingId = weddingId;
                addGuest.User = currentUser;    // <----well this took me 
                addGuest.WeddingPlan = wedding;    // 5ever to figure out lololol
            _context.Guests.Add(addGuest);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        




        [HttpGet("info/{infoId}")]
        public IActionResult Info(int infoId)
        {
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            if (currentUserId == null)
                {
                    return RedirectToAction("Index");
                }
            User currentUserInfo = _context.Users.Include(c => c.CreatedWeddings).FirstOrDefault(u => u.UserId == (int)currentUserId);
            ViewBag.currentUserInfo = currentUserInfo;
            WeddingPlan weddingInfo = _context.WeddingPlans.Include(g => g.GuestList).ThenInclude(u => u.User).FirstOrDefault(w => w.WeddingId == infoId);    
            return View(weddingInfo);
        }





        [HttpGet("plan")]
        public IActionResult Plan()
        {
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            if (currentUserId == null)
                {
                    return RedirectToAction("Index");
                }
            User currentUserInfo = _context.Users.Include(c => c.CreatedWeddings).FirstOrDefault(u => u.UserId == (int)currentUserId);
            ViewBag.currentUserInfo = currentUserInfo;
            return View();
        }





        [HttpPost("createplan")]
        public IActionResult CreatePlan(WeddingPlan FromForm)
        {     
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");  
            if (currentUserId == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            { 
                if(FromForm.WeddingDate < DateTime.Now)
                    {
                         ModelState.AddModelError("WeddingDate", "Date must be in the future");
                         return View("plan");
                    }
                else
                {
                    FromForm.UserId = (int)HttpContext.Session.GetInt32("currentUser");
                    _context.Add(FromForm);
                    _context.SaveChanges();

                    return RedirectToAction("info", new {infoId = FromForm.WeddingId});
                }
            }
            else
            {   
                return View("plan");
            }
        }





        [HttpGet("clear")]
            public IActionResult Logout()
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index");
            }

    }
}
