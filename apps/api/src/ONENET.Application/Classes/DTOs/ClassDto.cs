// QUAN-20260530-2302
using System;

namespace ONENET.Application.Classes.DTOs
{
    public class ClassDto
    {
        public Guid Id { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public string SchoolYear { get; set; }
        public int CurrentStudentCount { get; set; } // Sĩ số
        public Guid? HomeroomTeacherId { get; set; }
        public string? HomeroomTeacherName { get; set; }
    }
}