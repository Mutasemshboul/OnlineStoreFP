﻿using Microsoft.AspNetCore.Mvc;

namespace OnlineStoreFP.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
