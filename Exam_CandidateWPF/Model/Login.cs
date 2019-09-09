using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CandidateWPF.Model
{
    public class Login
    {
        int id;

        int userId;

        string username;

        string password;

        public Login(string Username, string Password)
        {  
            username = Username;
            password = Password;
        }

        public int Id { get => id; set => id = value; }
        public string UserId { get; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }
}
