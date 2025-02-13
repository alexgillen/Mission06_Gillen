using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission06_Gillen.Models;

namespace Mission06_Gillen.Controllers
{
    public class HomeController : Controller
    {
        private readonly AddMovieContext _context;

        public HomeController(AddMovieContext temp)
        {
            _context = temp;
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetToKnowJoel()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MovieForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MovieForm(AddMovie response)
        {
            _context.Movies.Add(response);
            _context.SaveChanges();

            return View("Confirmation", response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
