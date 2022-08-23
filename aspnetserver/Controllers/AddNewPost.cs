using aspnetserver.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace aspnetserver.Controllers
{
    public class AddNewPost
    {
        public class Command : IRequest<int>
        {
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
                    Title = request.Title,
                    Content = request.Content
                };

                await _db.Posts.AddAsync(entity, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);

                return entity.PostId;
            }
        }
    }
}
