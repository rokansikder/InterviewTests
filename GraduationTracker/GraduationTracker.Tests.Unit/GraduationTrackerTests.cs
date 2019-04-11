using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        private Diploma diploma;
        private Student[] students;
        Mock<IRepository> _repository;
        private GraduationTracker graduationTracker;

        
        [TestInitialize]
        public void Init()
        {
            _repository = new Mock<IRepository>();
            _repository.Setup(r => r.GetCourseRequirements()).Returns(GetRequirements());  
                        
            graduationTracker = new GraduationTracker(_repository.Object);

            diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            students = new[]
            {
               new Student
               {
                   Id = 1,
                   CourseMarks = new CourseMark[]
                   {
                        new CourseMark{CourseId = 1, Mark=95 },
                        new CourseMark{CourseId = 2, Mark=95 },
                        new CourseMark{CourseId = 3, Mark=95 },
                        new CourseMark{CourseId = 4, Mark=95 }
                   }
               },
               new Student
               {
                   Id = 2,
                   CourseMarks = new CourseMark[]
                   {
                        new CourseMark{CourseId = 1, Mark=80 },
                        new CourseMark{CourseId = 2, Mark=80 },
                        new CourseMark{CourseId = 3, Mark=80 },
                        new CourseMark{CourseId = 4, Mark=80 }
                   }
               },
            new Student
            {
                Id = 3,
                CourseMarks = new CourseMark[]
                {
                    new CourseMark{CourseId = 1, Mark=50 },
                    new CourseMark{CourseId = 2, Mark=50 },
                    new CourseMark{CourseId = 3, Mark=50 },
                    new CourseMark{CourseId = 4, Mark=50 }
                }
            },
            new Student
            {
                Id = 4,
                CourseMarks = new CourseMark[]
                {
                    new CourseMark{CourseId = 1, Mark=40 },
                    new CourseMark{CourseId = 2, Mark=40 },
                    new CourseMark{CourseId = 3, Mark=40 },
                    new CourseMark{CourseId = 4, Mark=40 }
                }
            }
        };
        }

       
        private Requirement[] GetRequirements()
        {
            return new[]
            {
                    new Requirement{Id = 100, MinimumMark=50, CourseId = 1, Credits=1 },
                    new Requirement{Id = 102, MinimumMark=50, CourseId = 2, Credits=1 },
                    new Requirement{Id = 103, MinimumMark=50, CourseId = 3, Credits=1},
                    new Requirement{Id = 104, MinimumMark=50, CourseId = 4, Credits=1 }
                };
        }
        
        [TestMethod]
        public void TestHasCredits()
        {            
            var graduated = new List<Tuple<bool, STANDING>>();

            foreach (var student in students)
            {
                var studentResult = graduationTracker.HasGraduated(diploma, student);
                if (studentResult.Item1)
                    graduated.Add(studentResult);
            }

            Assert.IsFalse(graduated.Count == 0);
        }

        [TestMethod]
        public void TestFailStudent()
        {
            int actualStudentCount = 1;
            int expectedStudentCount = 0;

            foreach (var student in students)
            {
                var studentResults = graduationTracker.HasGraduated(diploma, student);
                if (!studentResults.Item1)
                    expectedStudentCount++;
            }

            Assert.AreEqual(expectedStudentCount, actualStudentCount);
        }

        [TestMethod]
        public void TestGraduatedStudent()
        {
            int actualStudentCount = 3;
            int expectedStudentCount = 0;

            foreach (var student in students)
            {
                var studentResults = graduationTracker.HasGraduated(diploma, student);
                if (studentResults.Item1)
                    expectedStudentCount++;
            }

            Assert.AreEqual(expectedStudentCount, actualStudentCount);
        }

        [TestMethod]
        public void TestDiplomaNullTest()
        {
            Diploma diploma = null;
            var actualResult = graduationTracker.HasGraduated(diploma, students[0]);
            Tuple<bool, STANDING> expectedResult = new Tuple<bool, STANDING>(false, STANDING.None);

            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public void TestStudentNullTest()
        {
            var actualResult = graduationTracker.HasGraduated(diploma, null);
            Tuple<bool, STANDING> expectedResult = new Tuple<bool, STANDING>(false, STANDING.None);

            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public void TestStudentAndDiplomaNullTest()
        {
            Diploma nullDiploma = null;
            Student nullStudent = null;

            var actualResult = graduationTracker.HasGraduated(nullDiploma, nullStudent);
            Tuple<bool, STANDING> expectedResult = new Tuple<bool, STANDING>(false, STANDING.None);

            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}
