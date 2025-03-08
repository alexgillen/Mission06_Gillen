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


            //var movies = _context.Movies
            //    .Select(x => new AddMovie
            //    {
            //        MovieId = x.MovieId,
            //        Title = x.Title ?? "Unknown Title",
            //        Year = x.Year ?? "Unknown Year",
            //        Director = x.Director ?? "Unknown Director",
            //        Rating = x.Rating ?? "Unrated",
            //        Edited = x.Edited,
            //        CopiedToPlex = x.CopiedToPlex,
            //        LentTo = x.LentTo ?? "Not Lent",
            //        Notes = x.Notes ?? "No Notes"
            //    })
            //    .OrderBy(x => x.Title)
            //    .ToList();

            //return View(movies);
        }


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var recordToEdit = _context.Movies
        //        .FirstOrDefault(x => x.MovieId == id);
        //    return View("Edit", recordToEdit);


        //    //var recordToEdit = _context.Movies
        //    //    .Single(x => x.MovieId == id);

        //    //if (recordToEdit == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //ViewBag.Categories = _context.Categories
        //    //    .OrderBy(x => x.CategoryId)
        //    //    .ToList();

        //    //return View("MovieForm", recordToEdit);
        //}

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
                    existingRecord.Edited = updatedInfo.Edited;
                    existingRecord.CopiedToPlex = updatedInfo.CopiedToPlex;
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

        //[HttpPost]
        //public IActionResult Edit(AddMovie updatedInfo)
        //{
        //    _context.Update(updatedInfo);
        //    _context.SaveChanges();

        //    return RedirectToAction("MovieCollection");
        //}

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
