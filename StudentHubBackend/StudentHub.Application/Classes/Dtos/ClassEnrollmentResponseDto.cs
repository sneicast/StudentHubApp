using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Application.Classes.Dtos
{
    public class ClassEnrollmentResponseDto
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = default!;
        public string Message { get; set; } = default!;
        public int Credits { get; set; }
        public string ProfessorName { get; set; } = default!;
    }
}
