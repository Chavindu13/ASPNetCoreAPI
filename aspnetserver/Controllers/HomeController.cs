using aspnetserver.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        //public BookController(IMediator mediator) => _mediator = mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Post>> GetPostDetails() => await _mediator.Send(new GetPosts.Query());
        [HttpGet("{id}")]
        public async Task<Post> GetPostDetail(int id) => await _mediator.Send(new GetPostById.Query { Id = id });

        [HttpPost]
        public async Task<ActionResult> PostPostDetail([FromBody] AddNewPost.Command command)
        {
            var createdPostId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPostDetail), new { id = createdPostId }, null);
        }

        [HttpPut]
        public async Task<ActionResult> PutPostDetail([FromBody] UpdatePost.Command command)
        {
            var updatedPostId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPostDetail), new { id = updatedPostId }, null);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePostDetail(int id)
        {
            await _mediator.Send(new DeletePost.Command { Id = id });
            return NoContent();
        }
    }
}
