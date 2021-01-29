using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using caint.Models;

namespace caint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private readonly ThreadContext _context;

        public ThreadsController(ThreadContext context)
        {
            _context = context;
        }

        // GET: api/threads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThreadDTO>>> GetThread(string hostname, string pathname)
        {
            return await _context.threads.Select(x => ItemToDTO(x)).ToListAsync();
        }

        // GET: api/threads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThreadDTO>> GetComment(long id)
        {
            var thread = await _context.threads.FindAsync(id);

            if (thread == null)
            {
                return NotFound();
            }

            return ItemToDTO(thread);
        }

        // POST: api/threads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors("ExternalCORS")]
        [HttpPost]
        public async Task<long> NewThread(NewThreadDTO newThread)
        {
            var _threads = await _context.threads.ToListAsync();

            foreach (Thread eachThread in _threads)
            {
                
                if (eachThread.hostname == newThread.hostname && eachThread.path == newThread.path)
                {
                    return (eachThread.id);
                }
            }

            var thread = new Thread{
                hostname = newThread.hostname,
                path = newThread.path
            };
            
            _context.threads.Add(thread);
            await _context.SaveChangesAsync();

            return (ItemToDTO(thread).id);
        }

        private static ThreadDTO ItemToDTO(Thread thread) =>
        new ThreadDTO 
        {
            id = thread.id
        };

    }
}
