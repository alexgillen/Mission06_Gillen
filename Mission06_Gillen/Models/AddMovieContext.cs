using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission06_Gillen.Models;

namespace Mission06_Gillen.Models
{
    public class AddMovieContext : DbContext
    {
        public AddMovieContext(DbContextOptions<AddMovieContext> options) : base(options)
        {
        }

        public DbSet<AddMovie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AddMovie>().HasKey(m => m.MovieId);

        //    modelBuilder.Entity<Category>().HasData(
        //        new Category { CategoryId = 1, CategoryName = "Miscellaneous" },
        //        new Category { CategoryId = 2, CategoryName = "Comedy" },
        //        new Category { CategoryId = 3, CategoryName = "Drama" },
        //        new Category { CategoryId = 4, CategoryName = "Horror/Suspense" },
        //        new Category { CategoryId = 5, CategoryName = "Action/Adventure" },
        //        new Category { CategoryId = 6, CategoryName = "Television" },
        //        new Category { CategoryId = 7, CategoryName = "Family" },
        //        new Category { CategoryId = 8, CategoryName = "VHS" }
        //    );

        //    modelBuilder.Entity<AddMovie>().HasData(
        //        new AddMovie
        //        {
        //            //MovieId = Guid.NewGuid().ToString(), // Fixing the type conversion issue
        //            MovieId = -1,
        //            Title = "Sample Movie",
        //            Year = "2023",
        //            Director = "Sample Director",
        //            Rating = "PG",
        //            Edited = false,
        //            CopiedToPlex = false,
        //            LentTo = "Not Lent To",
        //            Notes = "No notes",
        //            CategoryId = 1
        //        }
        //    );
        //}
    }
}
