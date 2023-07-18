using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movie_base.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace movie_base.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private MoviesDbContext _dbContext;
        public MoviesController(ILogger<MoviesController> logger, MoviesDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        [HttpGet]
        public string Get()
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Error = (sender, args) =>
                {
                    args.ErrorContext.Handled = true;
                },
            };
            var allMovies = _dbContext.Movies.ToList();
            var res = JsonConvert.SerializeObject(allMovies, settings);
            Console.WriteLine(res);
            return res;
        }
        [HttpPost]
        [Route("add/")]
        public ActionResult Add([FromForm] RequestMovieModel requestMovieModel)
        {
            if (requestMovieModel == null)
                return BadRequest();
            var newMovie = new MovieModel();
            newMovie.Id = Guid.NewGuid();
            newMovie.Name = requestMovieModel.Name;
            newMovie.Description = requestMovieModel.Description;
            newMovie.RealeaseDate = DateTime.Parse(requestMovieModel.RealeaseDate);

            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", requestMovieModel.Image.FileName);
                newMovie.ImagePath = $"http://localhost:60060/Images/{requestMovieModel.Image.FileName}";

                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    requestMovieModel.Image.CopyTo(stream);
                }
            }
            catch(Exception ex)
            {
                return Content(ex.ToString());
            }

            _dbContext.Movies.Add(newMovie);
            _dbContext.SaveChanges();

            return Created(requestMovieModel.Name, requestMovieModel.Description);
        }
    }
}
