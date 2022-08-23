using aspnetserver.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostsContext _context;

        public PostController(PostsContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostDetails()
        {
            return await _context.Posts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostDetail(int id)
        {
            var postDetail = await _context.Posts.FindAsync(id);

            if (postDetail == null)
            {
                return NotFound();
            }

            return postDetail;
        }

        [HttpPost]
        public async Task<ActionResult<Post>> PostPostDetail(Post Post)
        {
            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPostDetail", new { id = Post.PostId }, Post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostDetail(int id)
        {
            var postDetail = await _context.Posts.FirstOrDefaultAsync(post => post.PostId == id); ;
            if (postDetail == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(postDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutPostDetail(Post Post)
        {
            _context.Entry(Post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (!PostDetailExists(Post.PostId))
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

        private bool PostDetailExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}
