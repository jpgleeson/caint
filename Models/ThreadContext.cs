using Microsoft.EntityFrameworkCore;

namespace caint.Models
{
    public class ThreadContext : DbContext
    {
        public ThreadContext(DbContextOptions<ThreadContext> options) : base(options)
        {

        }

        public DbSet<Thread> threads { get; set; }
    }
}