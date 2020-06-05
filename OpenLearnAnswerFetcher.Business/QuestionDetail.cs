using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Business
{
    public class QuestionDetail
    {
        public string Title { get; set; }

        public Collection<Choice> Choices { get; } = new Collection<Choice>();

        public class Choice
        {
            public bool IsCorrect { get; set; }
            public string Abc { get; set; }

            public string Text { get; set; }
        }
    }


    public class QuestionDetails : Collection<QuestionDetail>
    {
        public string CourseName { get; set; }

        public string ExecriseName { get; set; }
    }
}
