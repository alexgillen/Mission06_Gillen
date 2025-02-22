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
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View("MovieForm");
        }

        [HttpPost]
        public IActionResult MovieForm(AddMovie response)
        {
            response.MovieId = Guid.NewGuid().ToString(); // or any other unique value
            _context.Movies.Add(response);
            _context.SaveChanges();

            return View("Confirmation", response);
        }

        public IActionResult MovieCollection()
        {
            var movies = _context.Movies
                .Select(x => new AddMovie
                {
                    Title = x.Title ?? "Unknown Title",
                    Year = x.Year ?? "Unknown Year",
                    Director = x.Director ?? "Unknown Director",
                    Rating = x.Rating ?? "Unrated",
                    Edited = x.Edited,
                    CopiedToPlex = x.CopiedToPlex,
                    LentTo = x.LentTo ?? "Not Lent",
                    Notes = x.Notes ?? "No Notes"
                })
                .OrderBy(x => x.Title)
                .ToList();

            return View(movies);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Edit(int id)
        {
            var recordToEdit = _context.Categories
                .Single(x => x.CategoryId == id);

            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View("MovieForm", recordToEdit);
        }
        
    }
}
