using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CandidateWPF.Model
{
    public class question
    {
        int id;
        string questionq;
        int exam_id;

        public question(int Id, string Question, int Exam_id)
        {
            id = Id;
            questionq = Question;
            exam_id = Exam_id;
            
        }

        public string Question { get => questionq; }
        public int Id { get => id; }
        public int Exam_id{ get => exam_id; }

    }
}
