using aspnetserver.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace aspnetserver.Controllers
{
    public class UpdatePost
    {
        public class Command : IRequest<int>
        {
            public int PostId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly PostsContext _db;

            public CommandHandler(PostsContext db) => _db = db;

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new Post
                {
                    PostId = request.PostId,
                    Title = request.Title,
                    Content = request.Content
                };

                _db.Entry(entity).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return entity.PostId;
            }
        }
    }
}
