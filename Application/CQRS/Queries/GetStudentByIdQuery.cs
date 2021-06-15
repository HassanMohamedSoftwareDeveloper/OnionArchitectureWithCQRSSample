using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries
{
    public class GetStudentByIdQuery : IRequest<Student>
    {
        public int Id { get; set; }
        public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Student>
        {
            private readonly IAppDbContext context;
            public GetStudentByIdQueryHandler(IAppDbContext context)
            {
                this.context = context;
            }
            public async Task<Student> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
            {
                return await context.Students.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
            }
        }
    }
}
