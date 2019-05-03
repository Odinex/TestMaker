using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker
{
    class Question
    {
        public Int32 Number { get; set; }
        public Boolean isMultipleAnswer { get; set; }
        public String QuestionText { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
