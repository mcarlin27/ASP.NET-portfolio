using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class RepositoriesController : Controller
    {
        public IActionResult Index()
        {
            List<Repository> allRepositories = Repository.GetRepositories();
            return View(allRepositories);
        }
    }
}
