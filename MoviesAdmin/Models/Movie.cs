namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string AgeRating { get; set; } = string.Empty; // pg-13, R Etc

        public string Genre { get; set; } = string.Empty; //comedy, horror Etc

        public int Runtime { get; set; } // minutes, convert to hours if needed

        public DateTime Released {  get; set; }

        public string ImageFileName { get; set; } = string.Empty; // stores the rleated image, probably cover for the films

        public string Director { get; set; } = string.Empty;
    }
}
