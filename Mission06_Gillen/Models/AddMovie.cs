using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Mission06_Gillen.Models
{
    public class AddMovie
    {
        [Required]
        [Key]
        public int MovieId { get; set; }
        [Required(ErrorMessage = "Movie Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Year is required")]
        [Range(1888, int.MaxValue, ErrorMessage = "Year must be greater than or equal to 1888")]
        public int Year { get; set; }
        public string? Director { get; set; }
        [Required(ErrorMessage = "Rating is required")]
        public string Rating { get; set; }
        [Required(ErrorMessage = "Edited field is required")]
        public bool? Edited { get; set; }
        [Required(ErrorMessage = "CopiedToPlex is required")]
        public bool CopiedToPlex { get; set; }
        public string? LentTo { get; set; }
        public string? Notes { get; set; }


        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }

    //    public class AddMovie
    //{
    //    [ForeignKey("CategoryId")]
    //    public int CategoryId { get; set; }
    //    public Category Category { get; set; }

    //    [Required]
    //    [Key]
    //    public int MovieId { get; set; }

    //    [Required]
    //    public string Title { get; set; }
    //    [Required]
    //    [Range(1888, 2023)]
    //    public string Year { get; set; }
    //    [Required]
    //    public string Director { get; set; }
    //    public string? Rating { get; set; }
    //    [Required]
    //    public bool Edited { get; set; }
    //    [Required]
    //    public bool CopiedToPlex { get; set; }
    //    public string LentTo { get; set; }
    //    [MaxLength(25)]
    //    public string Notes { get; set; }
    //}
}
