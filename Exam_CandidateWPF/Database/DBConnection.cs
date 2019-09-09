using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam_CandidateWPF.Model;

namespace Exam_CandidateWPF.Database
{
    public static class DBConnection
    {
        public static MySqlDataReader Verbindung(string queryString)
        {


            MySqlConnection conn = new MySqlConnection(@"Server=10.160.14.110;Database=exam;port=3306;user ID=admin2; password=3dpo2; SslMode=none;");
            conn.Open();
            MySqlCommand command = new MySqlCommand(queryString, conn);
            MySqlDataReader reader = command.ExecuteReader();


            return reader;
        }

        public static Model.User DB_GetUser(int userId)
        {
            MySqlDataReader rdusers = Verbindung($"select * from user where id = {userId}");

            Model.User user = null;
            //ArbeitsspeicherListe = new List<Arbeitsspeicher>();
            //rdusers.
            int count = 0;
            while (rdusers.Read())
            {

                int id = (int)rdusers["id"];
                string firstname = (string)rdusers["firstname"];
                string lastname = (string)rdusers["lastname"];
                string email = (string)rdusers["email"];

                DateTime birthdate = Convert.ToDateTime(rdusers["birthdate"]);

                string plz = (string)rdusers["plz"];
                string street = (string)rdusers["street"];
                int streetnumber = (int)rdusers["streetnumber"];
                string city = (string)rdusers["city"];
                Boolean valid = (Boolean)rdusers["valid"];
                string voucher = (string)rdusers["voucher"];
                DateTime createdon = Convert.ToDateTime(rdusers["createdon"]);

                //Arbeispeicher erzeugen
                user = new Model.User(id, firstname, lastname, birthdate, email, plz, street,
                    streetnumber, city, valid,
                    voucher, createdon);

            }
            return user;
        }

        public static Boolean DB_CreateUser(string firstname, string lastname, DateTime birthdate, string email, string voucher,
            DateTime createdon, string city, string street, int streetnumber, string plz, bool valid, string password)
        {

            //create user;
            try
            {
                // Testen ob schon vorhanden
                int finduser = DB_FindUser(firstname, lastname, email);

                if (finduser == -1)
                {
                    string birthdateStr = Convert.ToString(birthdate);
                    string createdonStr = Convert.ToString(createdon);
                    MySqlDataReader rduser = Verbindung($"INSERT INTO user(firstname, lastname, email,birthdate, plz, street, " +
                    $"streetnumber, city, valid, voucher, createdon) " +
                    $"VALUES('{firstname}','{lastname}','{email}','{birthdateStr}','{plz}','{street}',{streetnumber},'{city}',{valid},'{voucher}','{createdonStr}')");

                    MySqlDataReader rdlastuser = Verbindung(@"select id from user ORDER BY id DESC LIMIT 1");

                    rdlastuser.Read();
                    int UserId = (int)rdlastuser["id"];

                    // create regsitration
                    DB_CreateLogin(UserId, email, password);
                    return true;
                }
                else return false;

            }
            catch (Exception ex)
            {
                return false;
                Console.WriteLine("Insert Prb" + ex.Message);
            }

        }

        private static void DB_CreateLogin(int userid, string email, string password)
        {
            MySqlDataReader rdlogin = Verbindung($"INSERT INTO login(password, userid, username) " +
           $"VALUES('{password}', {userid},'{email}')");
            string creatloginok = "OK";

        }


        public static Boolean DB_GetLogin(string username, string password, out Model.User user)
        {

            user = null;
            //ArbeitsspeicherListe = new List<Arbeitsspeicher>();
            //rdusers.

            bool login = false;
            try
            {
                MySqlDataReader rdlogin = Verbindung($"select * from login where username = '{username}' and  password = '{password}'");
                while (rdlogin.Read())
                {
                    int id = (int)rdlogin["id"];
                    int userId = (int)rdlogin["userId"];

                    user = DB_GetUser(userId);
                    //return true;
                    login = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Read Login Pb" + ex);
                user = null;
                return false;
            }
            return login;
        }

      

        public static int DB_FindUser(string firstname, string lastname, string email)
        {
            MySqlDataReader rduser = Verbindung($"select id from user where firstname = '{firstname}' and  lastname = '{lastname}' and  email = '{email}'");

            int userid = -1;

            while (rduser.Read())
            {

                userid = rduser.GetInt32(0);
            }
            return userid;
        }

        public static List<Model.question> DB_GetQuestions(string voucher, int userId, out Boolean voucherOK, 
            out Boolean questionsOK, out string examname_out)
        {
            List<Model.question> listfrage =new List<Model.question>();
            // Read voucher database

            MySqlDataReader rdvoucher = Verbindung($"select * from voucher where voucher = '{voucher}'");

            voucherOK = false;
            questionsOK = false;
            examname_out = null;

            while (rdvoucher.Read())
            {
                voucherOK = true;
                int id = (int)rdvoucher["id"];
                int examId = (int)rdvoucher["examId"];
                //examname_out = examId;

                // Update the Voucher the UserId
                MySqlDataReader rdupdatevoucher = Verbindung($"update voucher set userId='{userId}' where id = {id}");

                // Examname lesen
                MySqlDataReader rdexams= Verbindung($"select name from exams where id = '{examId}'");
                while (rdexams.Read())
                {
                     examname_out = (string)rdexams["name"];
                }
              // Get all questions
                    MySqlDataReader rdquestion = Verbindung($"select * from questions where exams_id = '{examId}'");
                while (rdquestion.Read())
                {
                    questionsOK = true;
                    int qid = (int)rdquestion["id"];
                    string question = (string)rdquestion["question"];
                    //string examname = (string)rdquestion["examname"];
                    Boolean valid = (bool)rdquestion["valid"];

                    if (valid) listfrage.Add(new Model.question(qid, question, examId));
                }
            }

            return listfrage;
        }

        public static void DB_SaveAnswers(List<Model.answer> listanswers)
        {
           
            foreach (var answer in listanswers)
            {
                MySqlDataReader wranswer = Verbindung($"INSERT INTO answers(createdon, examId,questionId, answer, note) " +
                        $"VALUES('{answer.Createdon}','{answer.Exam_id}','{answer.QuestionId}','{answer.Answerq}', '{answer.Note}')");
            }
        }

        public static int DB_GetResult(List<answer> listanswers)
        {
            int result = 0;
            MySqlDataReader rdanswers = Verbindung($"select note from answers");
            while (rdanswers.Read())
            {
                int note = (int)rdanswers["note"];
                result += note;
            }
            return result;
        }
    }
}


