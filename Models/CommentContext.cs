using Microsoft.EntityFrameworkCore;

namespace caint.Models
{
    public class CommentContext : DbContext
    {
        public CommentContext(DbContextOptions<CommentContext> options) : base(options)
        {

        }

        public DbSet<Comment> comments { get; set; }
    }
}