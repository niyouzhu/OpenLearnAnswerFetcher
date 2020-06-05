using OpenLearnAnswerFetcher.Business;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace OpenLearnAnswerFetcher.Client
{
    public class SimulatorViewModel
    {

        public string ExerciseName { get; set; }


        public ICollection<QuestionDetail> Questions { get; } = new Collection<QuestionDetail>();

        public class QuestionDetail
        {
            [DisplayName("题目")]
            public string Title { get; set; }
            [DisplayName("答案")]
            public string Answers { get; set; }

        }

        public static IEnumerable<SimulatorViewModel> ConvertFrom(IEnumerable<QuestionDetails> questionDetails)
        {
            var rt = new List<SimulatorViewModel>(questionDetails.Count());
            foreach (var questions in questionDetails)
            {
                var item = new SimulatorViewModel() { ExerciseName = questions.ExecriseName };
                foreach (var detail in questions)
                {
                    var correct = detail.Choices.Where(it => it.IsCorrect);
                    var answers = string.Join(Environment.NewLine, correct.Join(correct, c1 => c1.Text, c2 => c2.Text, (c1, c2) => $"{c1.Abc}:{c2.Text}"));
                    item.Questions.Add(new QuestionDetail() { Title = detail.Title, Answers = answers });
                }
                rt.Add(item);

            }
            return rt;
        }

    }

}