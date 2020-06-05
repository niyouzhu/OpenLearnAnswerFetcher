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
    public class QuestionDetailSimulator : Simulator<QuestionDetail>
    {
        public QuestionDetailSimulator(Agent agent, string uri, HttpMethod method, ICollection<KeyValuePair<string, string>> queryString, ICollection<KeyValuePair<string, string>> header) : base(agent, uri, method, queryString, header)
        {
        }

        public QuestionDetailSimulator(Agent agent, string uri, string itemBankId, string questionId, ICollection<KeyValuePair<string, string>> header) : this(agent, uri, HttpMethod.Get, new Collection<KeyValuePair<string, string>>() {
            new KeyValuePair<string, string>("bust", DateTime.Now.Ticks.ToString()),
                new KeyValuePair<string,string>("itemBankId",itemBankId),
                new KeyValuePair<string, string>("questionId",questionId),
        }, header)
        { }

        public QuestionDetailSimulator(Agent agent, string uri, string itemBankId, string questionId, string referer) : this(agent, uri, itemBankId, questionId, new Collection<KeyValuePair<string, string>>() {
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

        public QuestionDetailSimulator(Agent agent, string uri, string itemBankId, string questionId) : this(agent, uri, itemBankId, questionId, $"Referer: http://learn.open.com.cn/StudentCenter/OnLineJob/WrongBook")
        {
        }

        public QuestionDetailSimulator(Agent agent, string itemBankId, string questionId) : this(agent, @"http://learn.open.com.cn/StudentCenter/OnlineJob/GetQuestionDetail", itemBankId, questionId)
        {
        }

        public QuestionDetailSimulator(Agent agent, WrongQuestion exercise) : this(agent, exercise.ItemBankId, exercise.QuestionId)
        {

        }


        public override IEnumerable<QuestionDetail> Simulate(HttpContent content)
        {
            throw new NotImplementedException();

        }

        public override QuestionDetail SimulateOne(HttpContent content)
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
            if (obj.data == null) return null;
            var questionDetail = new QuestionDetail()
            {
                Title = obj.data.I2
            };
            foreach(var choice in obj.data.Choices)
            {
                questionDetail.Choices.Add(new QuestionDetail.Choice() { Abc = choice.I1, IsCorrect = choice.IsCorrect, Text = choice.I2 });

            };

            return questionDetail;
        }
    }
}
