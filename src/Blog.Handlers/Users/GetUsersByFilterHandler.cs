using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Blog.Handlers.Users
{
    public class GetUsersByFilterHandler : IRequestHandler<GetUsersByFilterRequest, GetUsersByFilterResult>
    {
        public Task<GetUsersByFilterResult> Handle(GetUsersByFilterRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}