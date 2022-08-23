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
    public class GetPosts
    {
        public class Query : IRequest<IEnumerable<Post>> { }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Post>>
        {
            private readonly PostsContext _db;

            public QueryHandler(PostsContext db) => _db = db;

            public async Task<IEnumerable<Post>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.Posts.ToListAsync(cancellationToken);
            }
        }
    }
}
