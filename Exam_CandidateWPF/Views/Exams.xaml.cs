using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Exam_CandidateWPF.Views
{
    /// <summary>
    /// Interaktionslogik für Exams.xaml
    /// </summary>
    public partial class Exams : Window
    {
        Model.User user;
        Boolean voucherOK = false;
        Boolean questionsOK = false;
        List<Model.question> listquestions;
        List<Model.answer> listanswers = new List<Model.answer>();
        Model.answer answer;
        int examId;
        string exam_name = null;
        Boolean updateReady = true;


        public Exams(Model.User User)
        {
            user = User;
            InitializeComponent();

            //get all questions via the voucher(id)
            BT_SendAnswers.IsEnabled = false;
            BT_Update.IsEnabled = false;
            listquestions = Database.DBConnection.DB_GetQuestions(user.Voucher, user.Id, out voucherOK, out questionsOK, out exam_name);



            if (!voucherOK)
            {
                TB_Message.Visibility = Visibility.Visible;
                TB_Message.Text = "Ihre Voucher ist ungültig!";
            }
            if (!questionsOK)
            {
                TB_Message.Visibility = Visibility.Visible;
                TB_Message.Text = "Ihre Voucher ist ungültig!";
            }

            if ((voucherOK) && (questionsOK))
            {
                //listanswers preparation
                foreach (var question in listquestions)
                {
                    listanswers.Add(new Model.answer(user.Id, question.Id, question.Question, String.Empty, question.Exam_id, DateTime.Now,0));
                }

                QS_Anzeige.ItemsSource = null;
                QS_Anzeige.ItemsSource = listanswers;

                BT_SendAnswers.IsEnabled = false;
                BT_Update.IsEnabled = true;
                TB_Username.Text = user.Email;
                TB_Voucher.Text = user.Voucher;
                TB_ExamName.Text = exam_name;
                QS_Anzeige.ItemsSource = null;
                QS_Anzeige.ItemsSource = listanswers;
            }


        }

        private void QS_Anzeige_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (QS_Anzeige.SelectedIndex != -1)
            {

                int auswahdg = QS_Anzeige.SelectedIndex;

                TB_Question.Text = listanswers[QS_Anzeige.SelectedIndex].Question;

                TB_Answer.Text = listanswers[QS_Anzeige.SelectedIndex].Answerq;

            }
        }
      

        private void BT_UpdateAnswer_Click(object sender, RoutedEventArgs e)
        {         
            int auswahdg = QS_Anzeige.SelectedIndex;
           
            if ((TB_Answer.Text != listanswers[QS_Anzeige.SelectedIndex].Answerq))
            {
                string answerAlt = listanswers[auswahdg].Answerq;
                Model.answer answerAs = new Model.answer(user.Id, listanswers[auswahdg].QuestionId, listanswers[auswahdg].Question, TB_Answer.Text,
                    listanswers[auswahdg].Exam_id, DateTime.Now, 0);
                //answer(int UserId, int QuestionId, string Question, string Answer, int Exam_id, DateTime Createdon, int Note)
                listanswers.RemoveAt(auswahdg);
                listanswers.Add(answerAs);
                QS_Anzeige.ItemsSource = null;
                QS_Anzeige.ItemsSource = listanswers;

            }
            updateReady = true;
            foreach (var answer in listanswers)
            {
                if (answer.Answerq.Length == 0) { updateReady = false; break; }
            }

            if (updateReady)
            {
                BT_SendAnswers.IsEnabled = true;

            }
        }

        private void BT_SendAnswers_Click(object sender, RoutedEventArgs e)
        {
            
            if (updateReady)
            {
                Database.DBConnection.DB_SaveAnswers(listanswers);
            }

            BT_Update.IsEnabled = false;
            BT_SendAnswers.IsEnabled = false;
            BT_Result.Visibility = Visibility.Visible;
            TB_Result.Visibility = Visibility.Visible;
            // this.Close();

        }

        private void BT_Result_Click(object sender, RoutedEventArgs e)
        {
           TB_Result.Text =  Convert.ToString(Database.DBConnection.DB_GetResult(listanswers));

        }
    }
}
