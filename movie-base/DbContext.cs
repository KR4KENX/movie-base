using Microsoft.EntityFrameworkCore;
using movie_base.Models;

namespace movie_base
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {

        }
        public DbSet<MovieModel> Movies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieModel>().ToTable(nameof(Movies));
        }
    }
}
