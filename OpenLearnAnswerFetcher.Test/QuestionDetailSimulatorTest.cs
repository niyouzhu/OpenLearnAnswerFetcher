using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenLearnAnswerFetcher.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Test
{
    [TestClass]
    public class QuestionDetailSimulatorTest
    {
        [TestMethod]
        public void TestSimulate()
        {
            string header;
            using (var stream = File.OpenText("./CourseListSimulatorHeader.txt"))
            {
                header = stream.ReadToEnd();
            }
            var domain = new Uri("http://learn.open.com.cn");
            var agent = new Agent(domain, header);
            var courseListSimulator = new CourseListSimulator(agent, "http://learn.open.com.cn/StudentCenter/MyWork/GetOnlineJsonAll?t=0.40281778743324614", HttpMethod.Get, "t =0.8017772560482458", header);
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
                var item = new QuestionDetails();
                foreach (var wrongQuestion in wrongQuestions)
                {
                    var questionDetailSimualtor = new QuestionDetailSimulator(agent, wrongQuestion);
                    var result = questionDetailSimualtor.SimulateOne(null);
                    item.Add(result);
                }
                rt.Add(item);
            }
            Assert.IsTrue(rt.Count > 0);
        }
    }
}
