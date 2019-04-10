using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public class Student
    {
        public int Id { get; set; }
        public CourseMark[] CourseMarks { get; set; }
        public STANDING Standing { get; set; } = STANDING.None;
    }
}
