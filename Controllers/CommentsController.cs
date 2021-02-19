using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using caint.Data;
using caint.Models;

namespace caint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly caintDBContext _context;

        public CommentsController(caintDBContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> Getcomments()
        {
            return await _context.comments.Select(x => ItemToDTO(x)).ToListAsync();
        }

        [HttpGet("admin")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsAdmin()
        {
            var commentList = await _context.comments.ToListAsync();

            return commentList;
        }

        [HttpGet("admin/{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsAdmin(long id)
        {
            var commentList = await _context.comments.Where(x => x.threadId == id).ToListAsync();

            Console.WriteLine(commentList.Count);

            return commentList;
        }

        [HttpPost("admin/approve/{id}")]
        public async Task<IActionResult> PutComment(long id)
        {
            var comment = await _context.comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            comment.approved = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetComment(long id)
        {
            var comment = await _context.comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return ItemToDTO(comment);
        }

        //[EnableCors]
        [HttpGet("thread/{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentThread(long id)
        {
            var comment = await _context.comments.ToListAsync();
            List<Comment> returnList = new List<Comment>();

            foreach (Comment singleComment in comment)
            {
                if (singleComment.threadId == id && singleComment.approved)
                {
                    returnList.Add(singleComment);
                }
            }

            if (comment == null)
            {
                return NotFound();
            }

            return returnList;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(long id, CommentDTO CommentDTO)
        {
            if (id != CommentDTO.id)
            {
                return BadRequest();
            }

            _context.Entry(CommentDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[EnableCors]
        [HttpPost]
        public async Task<ActionResult<CommentDTO>> PostComment(CommentDTO commentDTO)
        {
            var comment = new Comment
            {
                approved = false,
                name = commentDTO.name,
                body = commentDTO.body,
                threadId = commentDTO.threadId
            };
            
            _context.comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComment), new { id = comment.id }, ItemToDTO(comment));
        }


        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(long id)
        {
            var comment = await _context.comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(long id)
        {
            return _context.comments.Any(e => e.id == id);
        }

        private static CommentDTO ItemToDTO(Comment comment) =>
        new CommentDTO 
        {
            id = comment.id,
            name = comment.name,
            body = comment.body,
            threadId = comment.threadId
        };
    }
}
