using Microsoft.EntityFrameworkCore;
using caint.Models;

namespace caint.Data
{
    public class caintDBContext : DbContext
    {
        public caintDBContext(DbContextOptions<caintDBContext> options) : base(options)
        {

        }

        public DbSet<Comment> comments { get; set; }
        public DbSet<Thread> threads { get; set; }
    }
}