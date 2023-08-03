//using DeckardMvcDemoApp.DataAccess;
using DeckardMvcDemoApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace DeckardMvcDemoApp.Controllers
{
    public class HumanResourcesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Building()
        {
            return View();
        }

        public IActionResult Employee()
        {
            return View();
        }

        public IActionResult Office()
        {
            return View();
        }
    }
}
