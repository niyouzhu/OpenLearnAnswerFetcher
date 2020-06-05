using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Business
{
    public class WrongQuestion
    {


        public string BatchId { get; set; }

        public string LevelId { get; set; }

        public string SpecialtyId { get; set; }

        public string ItemBankId { get; set; }

        public string QuestionId { get; set; }

        public QuestionTypeInfomation QuestionTypeInfo { get; } = new QuestionTypeInfomation();

        public class QuestionTypeInfomation
        {
            public int QuestionInnerTypeId { get; set; }

            public string QuestionTypeId { get; set; }
            public string QuestionSectionId { get; set; }
            public string QuestionTypeName { get; set; }

        }
    }

    public class WrongQuestions : Collection<WrongQuestion>
    {
        public WrongQuestions(IEnumerable<WrongQuestion> source)
        {
            this.Add(source);
        }

        public WrongQuestions() : base()
        {

        }

        public string CourseName { get; set; }

        public string ExecriseName { get; set; }
    }
}
