using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        private Diploma diploma;
        private Student[] students;
        private GraduationTracker graduationTracker = new GraduationTracker();

        public GraduationTrackerTests()
        {
            PrepareTestData();
        }

        private void PrepareTestData()
        {
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

        [TestMethod]
        public void TestHasCredits()
        {
            var tracker = new GraduationTracker();
            var graduated = new List<Tuple<bool, STANDING>>();

            foreach (var student in students)
            {
                var studentResult = tracker.HasGraduated(diploma, student);
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
