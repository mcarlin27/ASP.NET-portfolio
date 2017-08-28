using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using System.Collections.Generic;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Repository> allRepositories = Repository.GetRepositories();
            return View(allRepositories);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
