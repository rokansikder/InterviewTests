using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public class Repository
    {
        public static Student GetStudent(int id)
        {
            var students = GetStudents();
            if (students == null) return null;

            return students.Where(s => s.Id == id).FirstOrDefault();
        }

        public static Diploma GetDiploma(int id)
        {
            var diplomas = GetDiplomas();
            if (diplomas == null) return null;

            return diplomas.Where(d => d.Id == id).FirstOrDefault();
        }

        public static Requirement GetRequirement(int id)
        {
            var requirements = GetRequirements();
            if (requirements == null) return null;

            return requirements.Where(r => r.Id == id).FirstOrDefault();
        }

        public static Requirement[] GetCourseRequirements()
        {
            return GetRequirements();
        }

        private static Course[] GetCourses()
        {
            return new Course[] {
                new Course{ Id = 1, Name ="Math"},
                new Course{ Id = 2, Name ="Science"},
                new Course{ Id = 3, Name ="Literature"},
                new Course{ Id = 4, Name ="Physichal Education"}
            };
        }

        private static Diploma[] GetDiplomas()
        {
            return new[]
            {
                new Diploma
                {
                    Id = 1,
                    Credits = 4,
                    Requirements = new int[]{100,102,103,104}
                }
            };
        }

        public static Requirement[] GetRequirements()
        {
            return new[]
            {
                    new Requirement{Id = 100, MinimumMark=50, CourseId = 1, Credits=1 },
                    new Requirement{Id = 102, MinimumMark=50, CourseId = 2, Credits=1 },
                    new Requirement{Id = 103, MinimumMark=50, CourseId = 3, Credits=1},
                    new Requirement{Id = 104, MinimumMark=50, CourseId = 4, Credits=1 }
                };
        }
        private static Student[] GetStudents()
        {
            return new[]
            {
               new Student
               {
                   Id = 1,
                   CourseMarks = new CourseMark[]{
                        new CourseMark{CourseId = 1, Mark = 95 },
                        new CourseMark{CourseId = 2, Mark = 95 },
                        new CourseMark{CourseId = 3, Mark = 95 },
                        new CourseMark{CourseId = 4, Mark = 95 }
                   }
               },
               new Student
               {
                   Id = 2,
                   CourseMarks = new CourseMark[]{
                        new CourseMark{CourseId = 1, Mark = 80 },
                        new CourseMark{CourseId = 2, Mark = 80 },
                        new CourseMark{CourseId = 3, Mark = 80 },
                        new CourseMark{CourseId = 4, Mark = 80 }
                   }
               },
            new Student
            {
                Id = 3,
                CourseMarks = new CourseMark[]{
                        new CourseMark{CourseId = 1, Mark=50 },
                        new CourseMark{CourseId = 2, Mark=50 },
                        new CourseMark{CourseId = 3, Mark=50 },
                        new CourseMark{CourseId = 4, Mark=50 }
                   }
            },
            new Student
            {
                Id = 4,
                CourseMarks = new CourseMark[]{
                        new CourseMark{CourseId = 1, Mark = 40 },
                        new CourseMark{CourseId = 2, Mark = 40 },
                        new CourseMark{CourseId = 3, Mark = 40 },
                        new CourseMark{CourseId = 4, Mark = 40 }
                   }
            }

            };
        }
    }
}
