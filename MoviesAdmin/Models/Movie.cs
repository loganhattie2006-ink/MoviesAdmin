namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Runtime { get; set; }
        
        public string AgeRating { get; set; }

        public DateTime Released {  get; set; }
    }
}
