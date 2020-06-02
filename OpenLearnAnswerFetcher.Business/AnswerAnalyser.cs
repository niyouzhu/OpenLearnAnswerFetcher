using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Business
{
    public class AnswerAnalyser
    {
        public AnswerAnalyser(string jsonString)
        {
            JsonString = jsonString;
        }
        public string JsonString { get; }

        public Results Analyse()
        {
            var fetcher = new OpenLearnHistoryDetailFetcher(JsonString);
            var obj = fetcher.Fetch();
            var questions = new List<Question>();
            foreach (var item in obj.data.paperInfo.Items)
            {
                questions.Add(new Question() { Id = item.I1, Title = item.I2 });
            }
            var answers = new List<Answer>();
            foreach (var item in obj.data.answerInfo.Items)
            {
                var result = new string[item.I15.Count];
                for (int i = 0; i < item.I15.Count; i++)
                {
                    result[i] = item.I15[i];
                }
                answers.Add(new Answer() { Id = item.I1, Result = result });
            }

            var results = new Results();
            foreach (var item in questions)
            {
                results.Add(new Result(item, answers.First(it => string.Equals(it.Id, item.Id, StringComparison.InvariantCultureIgnoreCase))));
            }
            return results;
        }
    }
}
