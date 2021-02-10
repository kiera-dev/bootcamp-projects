using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

namespace TimeMachine
{
    public class HomeController : Controller
    {
        [Route("")]  
        [HttpGet]   
        public IActionResult Index()
        {
            DateTime rightNow = DateTime.Now;
            ViewBag.Time = rightNow;
            return View(ViewBag.Time);
        }
        
    }
}
