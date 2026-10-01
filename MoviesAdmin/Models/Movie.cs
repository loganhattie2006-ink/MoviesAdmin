using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        [Required]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Age Rating")]
        [StringLength(10)]
        [Required]
        public string AgeRating { get; set; } = string.Empty; // pg-13, R Etc

        public string Genre { get; set; } = string.Empty; //comedy, horror Etc
        
        [Display(Name = "Runtime (Min)")]
        [Range(1,600)]
        [Required]
        public int Runtime { get; set; } // minutes, convert to hours if needed

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        [Required]
        public DateOnly Released {  get; set; }

        [Display(Name ="Poster file")]
        public string? ImageFileName { get; set; } = string.Empty; //cover for the films, nullable as some may not have a poster

        [StringLength(100)]
        [Required]
        public string Director { get; set; } = string.Empty; // Able to view the director to see other movies any other movies they were involved in
    }
}
