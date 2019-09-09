using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CandidateWPF.Model
{
    public class answer
    {
        int id;
        int questionId;
        int userId;
        string question;
        string answerq;
        int exam_id;
        DateTime createdon;
        int note;

        public answer(int UserId, int QuestionId, string Question, string Answer, int Exam_id, DateTime Createdon, int Note)
        {
            
            userId = UserId;
            question = Question;
            questionId = QuestionId;
            answerq = Answer;
            exam_id = Exam_id; 
            createdon = Createdon;
            note = Note;
        }
 
       
        public string Question { get => question; set => question = value; }
        public string Answerq { get => answerq; set => answerq = value; }
        public int Exam_id { get => exam_id; set => exam_id = value; }
        public DateTime Createdon { get => createdon; }
        public int Note { get => note; }
        public int UserId { get => userId; set => userId = value; }
        public int QuestionId { get => questionId; }
    }
}
