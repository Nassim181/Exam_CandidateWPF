using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CandidateWPF.Model
{
    public class voucher
    {
        int id;
        int examId;
        string vouchern;

        public voucher(int Id, int ExamId, string Vouchern)
        {
            id = Id;
            examId = ExamId;
            vouchern = Vouchern; 
        }

        public int Id { get => id; set => id = value; }
        public int ExamId { get => examId; set => examId = value; }
        public string Vouchern { get => vouchern; set => vouchern = value; }
    }
}
