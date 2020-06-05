using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Business
{
    public class Worker
    {
        public IEnumerable<QuestionDetails> Work(string header)
        {
            var domain = new Uri("http://learn.open.com.cn");
            var agent = new Agent(domain, header);
            var courseListSimulator = new CourseListSimulator(agent);
            var courses = courseListSimulator.Simulate(null);
            var wrongQuestionsList = new List<WrongQuestions>();
            foreach (var course in courses)
            {
                foreach (var exercise in course.Exercises)
                {
                    var wrongQuestionSimulator = new WrongQuestionSimulator(agent, exercise);
                    var result = wrongQuestionSimulator.Simulate(null);
                    if (result.Count() != 0)
                    {
                        var wrongQuestions = new WrongQuestions(result) { ExecriseName = exercise.Name, CourseName = course.CourseName };
                        wrongQuestionsList.Add(wrongQuestions);
                    }

                }
            }

            var rt = new List<QuestionDetails>();
            foreach (var wrongQuestions in wrongQuestionsList)
            {
                var item = new QuestionDetails() { CourseName = wrongQuestions.CourseName, ExecriseName = wrongQuestions.ExecriseName };
                foreach (var wrongQuestion in wrongQuestions)
                {
                    var questionDetailSimualtor = new QuestionDetailSimulator(agent, wrongQuestion);
                    var result = questionDetailSimualtor.SimulateOne(null);
                    item.Add(result);
                }
                rt.Add(item);
            }
            return rt;
        }
    }
}
