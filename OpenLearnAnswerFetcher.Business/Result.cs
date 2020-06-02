using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Business
{
    public class Result
    {
        public Result(Question question, Answer answer)
        {
            Question = question;
            Answer = answer;
        }

        public Question Question { get; }
        public Answer Answer { get; }
    }

    public class Results : Collection<Result>
    {

    }
}
