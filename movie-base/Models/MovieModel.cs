namespace movie_base.Models
{
    public class MovieModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime RealeaseDate { get; set; }
        public string ImagePath { get; set; }
    }
    public class RequestMovieModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string RealeaseDate { get; set; }
        public IFormFile Image { get; set; }
    }
}
