using MediiProgramareEntity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MediiProgramareEntity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}