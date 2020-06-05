using OpenLearnAnswerFetcher.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Client
{
    public class FetcherViewModel
    {

        public string Id { get; set; }
        [DisplayName("题目")]
        [Order(1)]
        public string Title { get; set; }
        [DisplayName("答案")]
        [Order(2)]
        public string[] Answers { get; set; }

        public static IEnumerable<FetcherViewModel> GetResults(string jsonString)
        {
            var analyser = new AnswerAnalyser(jsonString);
            var results = analyser.Analyse();

            var rt = new List<FetcherViewModel>(results.Count);
            foreach (var item in results)
            {

                rt.Add(new FetcherViewModel() { Id = item.Question.Id, Title = item.Question.Title, Answers = item.Answer.Result });
            }
            return rt;

        }

    }

    public class FetcherExcelViewModel
    {
        [DisplayName("题目")]
        [Order(1)]
        public string Title { get; set; }
        [DisplayName("答案")]
        [Order(2)]
        public string Answers { get; set; }

        public FetcherExcelViewModel(FetcherViewModel model)
        {
            Title = model.Title;
            foreach (var item in model.Answers)
            {
                switch (item)
                {
                    case "0":
                        Answers = $"{Answers},A";
                        break;
                    case "1":
                        Answers = $"{Answers},B";
                        break;
                    case "2":
                        Answers = $"{Answers},C";
                        break;
                    case "3":
                        Answers = $"{Answers},D";
                        break;
                    default:
                        Answers = $"{Answers},{item}";
                        break;

                }
            }
            if (Answers.First() == ',')
            {
                Answers = Answers.Substring(1);
            }

        }

        public static IEnumerable<FetcherExcelViewModel> ConvertFrom(IEnumerable<FetcherViewModel> source)
        {
            var rt = new List<FetcherExcelViewModel>(source.Count());
            foreach (var item in source)
            {
                rt.Add(new FetcherExcelViewModel(item));
            }
            return rt;
        }
    }
}
