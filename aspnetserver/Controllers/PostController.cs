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
    }
}
