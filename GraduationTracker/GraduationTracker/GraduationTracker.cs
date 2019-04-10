using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {
        public Tuple<bool, STANDING> HasGraduated(Diploma diploma, Student student)
        {
            Tuple<bool, STANDING> result = new Tuple<bool, STANDING>(false, STANDING.None);

            if (diploma == null || student == null || diploma.Requirements == null) return result;

            var credits = 0;
            int totalMarks = 0;
            var courseRequirements = Repository.GetCourseRequirements();

            for (int i = 0; i < diploma.Requirements.Length; i++)
            {
                var courseRequirement = courseRequirements.Where(cr => cr.Id == diploma.Requirements[i]).FirstOrDefault();
                if (courseRequirement == null) continue;

                var courseMark = student.CourseMarks.Where(m => m.CourseId == courseRequirement.CourseId).FirstOrDefault();
                if (courseMark == null) continue;

                totalMarks += courseMark.Mark;

                if (courseMark.Mark > courseRequirement.MinimumMark)
                    credits += courseRequirement.Credits;
            }

            var average = totalMarks / student.CourseMarks.Length;

            if (average < 50)
                result = new Tuple<bool, STANDING>(false, STANDING.Remedial);
            else if (average < 80)
                result = new Tuple<bool, STANDING>(true, STANDING.Average);
            else if (average < 95)
                result = new Tuple<bool, STANDING>(true, STANDING.MagnaCumLaude);
            else
                result = new Tuple<bool, STANDING>(true, STANDING.SumaCumLaude);

            return result;
        }
    }
}
