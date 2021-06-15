using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands
{
    public class CreateStudentCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Standard { get; set; }
        public int Rank { get; set; }

        public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand,int>
        {
            private readonly IAppDbContext context;
            public CreateStudentCommandHandler(IAppDbContext context)
            {
                this.context = context;
            }
            public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
            {
                var student = new Student();
                student.Name = request.Name;
                student.Standard = request.Standard;
                student.Rank = request.Rank;

                context.Students.Add(student);
                await context.SaveChangesAsync();
                return student.Id;
            }
        }
    }
}
