using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Business
{
    public class WrongQuestionSimulator : Simulator<WrongQuestion>
    {
        public WrongQuestionSimulator(Agent agent, string uri, HttpMethod method, ICollection<KeyValuePair<string, string>> queryString, ICollection<KeyValuePair<string, string>> header) : base(agent, uri, method, queryString, header)
        {
        }

        public WrongQuestionSimulator(Agent agent, string uri, string courseId, int courseExerciseId, string studentHomeworkId, string homeCourseId, ICollection<KeyValuePair<string, string>> header) : this(agent, uri, HttpMethod.Get, new Collection<KeyValuePair<string, string>>() {
            new KeyValuePair<string, string>("bust", DateTime.Now.Ticks.ToString()),
                new KeyValuePair<string,string>("courseid",courseId),
                new KeyValuePair<string, string>("courseExerciseId",courseExerciseId.ToString()),
                new KeyValuePair<string, string>("studentHomeworkId", studentHomeworkId),
                new KeyValuePair<string, string>("homeCourseId", homeCourseId)
        }, header)
        { }

        public WrongQuestionSimulator(Agent agent, string uri, string courseId, int courseExerciseId, string studentHomeworkId, string homeCourseId, string referer) : this(agent, uri, courseId, courseExerciseId, studentHomeworkId, homeCourseId, new Collection<KeyValuePair<string, string>>() {
           new KeyValuePair<string,string>("Accept", @"application/json, text/javascript, */*; q=0.01"),
           new KeyValuePair<string, string>("Accept-Encoding","gzip, deflate"),
           new KeyValuePair<string, string>("Accept-Language","zh-CN,en-US;q=0.7,en;q=0.3"),
           new KeyValuePair<string, string>("Cache-Control","no-cache"),
           new KeyValuePair<string, string>("Connection","keep-alive"),
           new KeyValuePair<string, string>("Host","learn.open.com.cn"),
           new KeyValuePair<string, string>("Pragma","no-cache"),
            new KeyValuePair<string, string>("User-Agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:76.0) Gecko/20100101 Firefox/76.0"),
            new KeyValuePair<string, string>("X-Requested-With","XMLHttpRequest"),
            new KeyValuePair<string, string>("Referer", referer)
        })

        { }

        public WrongQuestionSimulator(Agent agent, string uri, string courseId, int courseExerciseId, string studentHomeworkId, string homeCourseId) : this(agent, uri, courseId, courseExerciseId, studentHomeworkId, homeCourseId, $"http://learn.open.com.cn/StudentCenter/OnLineJob/WrongBook?courseid={courseId}&courseExerciseID={courseExerciseId}&studentHomeworkId={studentHomeworkId}&homeCourseId={homeCourseId}")
        {
        }

        public WrongQuestionSimulator(Agent agent, string courseId, int courseExerciseId, string studentHomeworkId, string homeCourseId) : this(agent, @"http://learn.open.com.cn/StudentCenter/OnlineJob/GetWrongQuestions", courseId, courseExerciseId, studentHomeworkId, homeCourseId)
        {
        }

        public WrongQuestionSimulator(Agent agent, Exercise exercise) : this(agent, exercise.CourseId, exercise.CourseExerciseId, exercise.StudentHomeworkId, exercise.HomeCourseId)
        {

        }


        public override IEnumerable<WrongQuestion> Simulate(HttpContent content)
        {
            var request = new HttpRequestMessage(Method, Uri);
            foreach (var item in Header)
            {
                request.Headers.Add(item.Key, item.Value);
            }
            request.Content = content;

            var response = Agent.Request(request);
            var task = response.Content.ReadAsStringAsync();
            task.Wait();
            dynamic obj = JsonConvert.DeserializeObject(task.Result);
            var rt = new List<WrongQuestion>();
            if (obj.data == null) return rt;
            foreach (var j in obj.data.Rows)
            {
                var wrongQuestion = new WrongQuestion()
                {
                    BatchId = j.BatchId,
                    ItemBankId = j.ItemBankId,
                    LevelId = j.LevelId,
                    QuestionId = j.QuestionId,
                    SpecialtyId = j.SpecialtyId,
                };
                wrongQuestion.QuestionTypeInfo.QuestionInnerTypeId = j.questionTypeInfo.questionInnerTypeId;
                wrongQuestion.QuestionTypeInfo.QuestionSectionId = j.questionTypeInfo.questionSectionId;
                wrongQuestion.QuestionTypeInfo.QuestionTypeId = j.questionTypeInfo.questionTypeId;
                wrongQuestion.QuestionTypeInfo.QuestionTypeName = j.questionTypeInfo.questionTypeName;
                rt.Add(wrongQuestion);

            }
            return rt;
        }

        public override WrongQuestion SimulateOne(HttpContent content)
        {
            throw new NotImplementedException();
        }
    }
}
