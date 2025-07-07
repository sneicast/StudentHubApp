using MediatR;

namespace StudentHub.Application.Classes.Commands
{
    public class RemoveStudentClassCommand : IRequest<Unit>
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
    }
}
