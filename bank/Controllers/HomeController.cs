using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Bank.Models;
using Microsoft.AspNetCore.Identity;


namespace Bank.Controllers
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
                    HttpContext.Session.SetInt32("currentUser", userInDb.UserId);
                    int? currentUserId = HttpContext.Session.GetInt32("currentUser");
                    return RedirectToAction("Account");
                }
            }
            return RedirectToAction("Login");
            }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View("register");
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
                FromForm.CashMoney = 0;
                _context.Add(FromForm);
                _context.SaveChanges();
                 
                var user = _context.Users.FirstOrDefault(u => u.Email == FromForm.Email);
                int currentUser = user.UserId;
                HttpContext.Session.SetInt32("currentUser", currentUser);
                int? currentUserId = HttpContext.Session.GetInt32("currentUser");
                return RedirectToAction("Account");
            } 
            else
            {
                return View("Register");     
            }
        }
    

        [HttpGet("account")]
        public IActionResult Account()
        {
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            if (currentUserId == null)
                {
                    return RedirectToAction("Index");
                }
            User currentUserInfo = _context.Users.Include(t => t.Transactions).FirstOrDefault(u => u.UserId == (int)currentUserId);
            ViewBag.currentUserInfo = currentUserInfo;
            var UserTransactions = currentUserInfo.Transactions.OrderByDescending(o => o.CreatedAt);  //I dont know why but this feels gross.
            ViewBag.UserTransactions = UserTransactions;     
            return View();
        }


        [HttpPost("withdraw")]
        public IActionResult Withdraw(Transaction FromForm)
        {
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            if (currentUserId == null)
                {
                    return RedirectToAction("Index");
                }
            User currentUserInfo = _context.Users.Include(t => t.Transactions).FirstOrDefault(u => u.UserId == (int)currentUserId);
            ViewBag.currentUserInfo = currentUserInfo;
            var UserTransactions = currentUserInfo.Transactions.OrderByDescending(o => o.CreatedAt); 
            ViewBag.UserTransactions = UserTransactions;   
            if(ModelState.IsValid)
            {
                User currentUser = _context.Users.FirstOrDefault(u => u.UserId == (int)currentUserId);
                if(currentUser.CashMoney < FromForm.Amount)
                {
                    ModelState.AddModelError("Amount", "Insert polite way to say 'you need more money' here");
                    return View("account");
                }
                else
                {
                    decimal newBalance = currentUser.CashMoney -= FromForm.Amount;
                    currentUser.CashMoney = newBalance;
                    _context.SaveChanges();

                    FromForm.UserId = (int)currentUserId;
                    FromForm.Type = "Withdrawal";
                    _context.Add(FromForm);
                    _context.SaveChanges();
                    return RedirectToAction("Account");
                }
            }
            return View("account");
        }



        [HttpPost("deposit")]
        public IActionResult Deposit(Transaction FromForm)
        {
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            if (currentUserId == null)
                {
                    return RedirectToAction("Index");
                }
            User currentUserInfo = _context.Users.Include(t => t.Transactions).FirstOrDefault(u => u.UserId == (int)currentUserId);
            ViewBag.currentUserInfo = currentUserInfo;
            var UserTransactions = currentUserInfo.Transactions.OrderByDescending(o => o.CreatedAt); 
            ViewBag.UserTransactions = UserTransactions;   
            if(ModelState.IsValid)
            {
                decimal newBalance = currentUserInfo.CashMoney += FromForm.Amount;
                currentUserInfo.CashMoney = newBalance;
                _context.SaveChanges();
                FromForm.UserId = (int)currentUserId;
                FromForm.Type = "Deposit";
                _context.Add(FromForm);
                _context.SaveChanges();
               return RedirectToAction("Account");
            }
            return View("Account");
        }


        [HttpGet("clear")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
