using aspnetserver.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace aspnetserver.Controllers
{
    public class GetPostById
    {
        public class Query : IRequest<Post>
        {
            public int Id { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Post>
        {
            private readonly PostsContext _db;

            public QueryHandler(PostsContext db) => _db = db;

            public async Task<Post> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.Posts.FindAsync(request.Id);
            }
        }
    }
}
