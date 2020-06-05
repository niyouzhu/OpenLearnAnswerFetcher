using Newtonsoft.Json;
using OpenLearnAnswerFetcher.Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Business
{
    public class CourseListSimulator : Simulator<Course>
    {
        public CourseListSimulator(Agent agent, string uri, HttpMethod method, ICollection<KeyValuePair<string, string>> queryString, ICollection<KeyValuePair<string, string>> header) : base(agent, uri, method, queryString, header)
        {
        }

        public CourseListSimulator(Agent agent, string uri, ICollection<KeyValuePair<string, string>> header) : this(agent, uri, HttpMethod.Get, new Collection<KeyValuePair<string, string>>() {
            new KeyValuePair<string, string>("t", DateTime.Now.Ticks.ToString())
                       }, header)
        { }

        public CourseListSimulator(Agent agent, string uri, string referer) : this(agent, uri, new Collection<KeyValuePair<string, string>>() {
           new KeyValuePair<string,string>("Accept", @"*/*"),
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

        public CourseListSimulator(Agent agent, string uri) : this(agent, uri, @"http://learn.open.com.cn/StudentCenter/MyLearning/MyWork")
        {
        }

        public CourseListSimulator(Agent agent) : this(agent, @"http://learn.open.com.cn/StudentCenter/MyWork/GetOnlineJsonAll")
        {
        }


        public CourseListSimulator(Agent agent, string uri, HttpMethod method, string queryString, string header) : base(agent, uri, method, queryString, header)
        {
        }

        public override IEnumerable<Course> Simulate(HttpContent content)
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
            var rt = new List<Course>();
            foreach (var j in obj.data.listWork)
            {
                var course = new Course();
                rt.Add(course);
                course.CourseName = j.CourseName;
                var exercises = new List<Exercise>();
                foreach (var k in j.Data)
                {
                    exercises.Add(new Exercise() { Name = k.ExerciseName, CourseExerciseId = k.CourseExerciseID, CourseId = k.CourseID, DownloadToken = k.downloadtoken, ExerciseType = k.ExerciseType, HomeCourseId = k.homeCourseId, HomeworkId = k.homeworkId, StudentHomeworkId = k.studentHomeworkId });

                }
                course.Exercises.Add(exercises);


            }
            return rt;
        }

        public override Course SimulateOne(HttpContent content)
        {
            throw new NotImplementedException();
        }
    }
}
