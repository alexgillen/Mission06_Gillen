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
    }
}
