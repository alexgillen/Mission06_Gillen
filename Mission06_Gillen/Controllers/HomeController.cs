using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            return View("MovieForm", new AddMovie());
        }

        [HttpPost]
        public IActionResult MovieForm(AddMovie response)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(response);
                _context.SaveChanges();

                return View("Confirmation", response);
            }
            else
            {
                ViewBag.Categories = _context.Categories
                    .OrderBy(x => x.CategoryName)
                    .ToList();

                return View(response);
            }
            
        }

        public IActionResult MovieCollection()
        {
            var movies = _context.Movies
                .Include(x => x.Category)
                .OrderBy(x => x.Title)
                .ToList();

            return View(movies);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AddMovie updatedInfo)
        {
            Console.WriteLine($"Received ID: {id}, Movie ID: {updatedInfo.MovieId}");

            if (id != updatedInfo.MovieId)
            {
                Console.WriteLine("Error: ID mismatch");
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingRecord = _context.Movies
                        .FirstOrDefault(x => x.MovieId == id);

                    if (existingRecord == null)
                    {
                        Console.WriteLine("Error: Movie not found in DB");
                        return NotFound();
                    }

                    existingRecord.Title = updatedInfo.Title;
                    existingRecord.Year = updatedInfo.Year;
                    existingRecord.Director = updatedInfo.Director;
                    existingRecord.Rating = updatedInfo.Rating;
                    existingRecord.Edited = updatedInfo.Edited;
                    existingRecord.LentTo = updatedInfo.LentTo;
                    existingRecord.CopiedToPlex = updatedInfo.CopiedToPlex;
                    existingRecord.Notes = updatedInfo.Notes;
                    existingRecord.CategoryId = updatedInfo.CategoryId;

                    _context.SaveChanges();
                    Console.WriteLine("Success: Record updated");

                    return RedirectToAction(nameof(MovieCollection));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database update Failed: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("ModelState is invalid");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validaiton Error: {error.ErrorMessage}");
                }
            }

            return View(updatedInfo);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordToDelete = _context.Movies
                .Single(x => x.MovieId == id);

            return View(recordToDelete);
        }

        [HttpPost]
        public IActionResult Delete(AddMovie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return RedirectToAction("MovieCollection");
        }
    }
}
