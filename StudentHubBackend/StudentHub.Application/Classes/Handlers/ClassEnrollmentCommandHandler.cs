

using MediatR;
using StudentHub.Application.Classes.Commands;
using StudentHub.Application.Classes.Dtos;
using StudentHub.Application.Classes.Interfaces;
using StudentHub.Application.Students.Interfaces;
using StudentHub.Domain.Entities;

namespace StudentHub.Application.Classes.Handlers
{
    public class ClassEnrollmentCommandHandler : IRequestHandler<ClassEnrollmentCommand, ClassEnrollmentResponseDto>
    {
        private readonly IEnrollmentsRepository _enrollmentsRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IClassesRepository _classesRepository;

        public ClassEnrollmentCommandHandler(IEnrollmentsRepository enrollmentsRepository, IStudentRepository studentRepository, IClassesRepository classesRepository)
        {
            _enrollmentsRepository = enrollmentsRepository;
            _studentRepository = studentRepository;
            _classesRepository = classesRepository;

        }
        public async Task<ClassEnrollmentResponseDto> Handle(ClassEnrollmentCommand request, CancellationToken cancellationToken)
        {
            Student student = GetDetailStudent(request.StudentId, cancellationToken);

            Class detailtClass = GetDetailClass(request.ClassId, cancellationToken);

            if (student.Enrollments.Any(e => e.ClassId == request.ClassId))
            {
                throw new InvalidOperationException("El estudiante ya está inscrito en esta clase.");
            }
            if (IsStudentAlreadyEnrolledInClassWithProfessor(student, detailtClass))
            {
                throw new InvalidOperationException("El estudiante ya está inscrito en una clase con el mismo profesor.");
            }

            int classCredits = detailtClass.Subject?.Credits ?? 0;
            if (CreditsExceedProgramLimit(student, classCredits))
            {
                throw new InvalidOperationException("Los créditos totales de la clase exceden el límite del programa.");
            }


            Enrollment newEnrollment = new Enrollment
            {
                StudentId = request.StudentId,
                ClassId = request.ClassId
            };


            await _enrollmentsRepository.EnrollStudentInClassAsync(newEnrollment, cancellationToken);

            Enrollment existingEnrollment = GetDetailtEnrollment(request.StudentId, request.ClassId, cancellationToken);

            return new ClassEnrollmentResponseDto
            {
                ClassId = existingEnrollment.ClassId,
                ClassName = existingEnrollment.Class?.Subject?.Name ?? "",
                Credits = existingEnrollment.Class?.Subject?.Credits ?? 0,
                Message = "Registro a clase exitoso",
                ProfessorName = existingEnrollment.Class?.Professor?.Name ?? ""
            };


        }

        //Validar si el estudiante ya tiene clase con el profesor que dicta la clase. el estudiante solo puede tener una clase por profesor, pero el profesor puede tener muchas class
        private bool IsStudentAlreadyEnrolledInClassWithProfessor(Student student, Class detailtClass)
        {
            int professorId = detailtClass.Professor?.Id ?? 0;
            return student.Enrollments.Any(e => e.Class?.ProfessorId == professorId);
        }

        private Student GetDetailStudent(int studentId, CancellationToken cancellationToken)
        {
            return _studentRepository.GetStudentByIdAsync(studentId, cancellationToken)
                .GetAwaiter().GetResult() ?? throw new InvalidOperationException("Estudiante no encontrado");
        }
        private Class GetDetailClass(int classId, CancellationToken cancellationToken)
        {
            return _classesRepository.GetClassByIdAsync(classId, cancellationToken)
                .GetAwaiter().GetResult() ?? throw new InvalidOperationException("La clase no se ha encontrado");
        }
        private Enrollment GetDetailtEnrollment(int studentId, int classId, CancellationToken cancellationToken)
        {
            return _enrollmentsRepository.GetEnrollmentAsync(studentId, classId, cancellationToken)
                .GetAwaiter().GetResult() ?? throw new InvalidOperationException("No se encontro el registro a la clase indicada. Intente registrarla nuevamente");
        }

        private bool CreditsExceedProgramLimit(Student student, int classCredits)
        {
            int totalCreditsProgram = student.CreditProgram?.TotalCredits ?? 0;
            int currentCredits = student.Enrollments
                .Where(e => e.Class?.Subject != null)
                .Sum(e => e.Class.Subject.Credits);
            return (currentCredits + classCredits) > totalCreditsProgram;
        }
    }
}
