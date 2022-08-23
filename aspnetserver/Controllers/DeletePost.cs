using aspnetserver.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace aspnetserver.Controllers
{
    public class DeletePost
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly PostsContext _db;

            public CommandHandler(PostsContext db) => _db = db;

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var book = await _db.Posts.FindAsync(request.Id);
                if (book == null) return Unit.Value;

                _db.Posts.Remove(book);
                await _db.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
