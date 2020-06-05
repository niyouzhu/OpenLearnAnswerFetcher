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
    public class WrongQuestionSimulatorTest
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
                    var wrongQuestions = new WrongQuestions();
                    wrongQuestions.Add(result);
                    wrongQuestionsList.Add(wrongQuestions);
                }
            }
        }
    }
}
