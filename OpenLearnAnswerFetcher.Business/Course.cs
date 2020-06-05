using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Business
{
    public class Course
    {
        public string CourseName { get; set; }

        public ICollection<Exercise> Exercises { get; } = new Collection<Exercise>();

    }

    public class Exercise
    {
        public int CourseExerciseId { get; set; }
        public string CourseId { get; set; }
        public int ExerciseType { get; set; }

        public string Name { get; set; }

        public string StudentHomeworkId { get; set; }
        public string HomeworkId { get; set; }
        public string DownloadToken { get; set; }
        public string HomeCourseId { get; set; }
    }
}
