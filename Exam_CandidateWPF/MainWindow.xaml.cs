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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exam_CandidateWPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Model.Login login;
        public Model.User user;
        public List<Model.User> userlist;
        Boolean loginas = false;
        public MainWindow()
        {

            //LB_Logged.IsVisible = false;
            //   LB_Logged.Visibility =  Visibility.Hidden;
            //  TB_Username.Visibility = Visibility.Hidden;
            //  BT_Logout.Visibility = Visibility.Hidden;
            //  BT_ExamCall.Visibility = Visibility.Hidden;
            //  BT_Result.Visibility = Visibility.Hidden;
            userlist = new List<Model.User>();
        }

        public void BT_Login_Click(object sender, RoutedEventArgs e)
        {
            //TB_Message.Visibility = Visibility.Hidden;

            try
            {
                TB_Message.Visibility = Visibility.Hidden;

                string username = TB_Username.Text;

                string password = TB_Password.Password.ToString();

                

                login = new Model.Login(username, password);

                Boolean LoginOK = Database.DBConnection.DB_GetLogin(username, password, out user);

                if (LoginOK)
                {
                    TB_UsernameDisplay.Text = user.Email;
                    LB_Logged.Visibility = Visibility.Visible;
                    //LB_Logged.Content = username;

                    BT_ExamCall.Visibility = Visibility.Visible;
                    BT_ExamCall.IsEnabled = true;
                    BT_Result.Visibility = Visibility.Visible;
                    BT_Result.IsEnabled = false;
                    BT_Login.IsEnabled = false;
                    BT_Register.IsEnabled = false;
                    TB_UsernameDisplay.Visibility = Visibility.Visible;
                    BT_Logout.Visibility = Visibility.Visible;
                    userlist.Add(user); 
                    DG_User.ItemsSource = null;
                    DG_User.ItemsSource = userlist;
                }
                else
                {
                   
                        TB_Message.Visibility = Visibility.Visible;
                        TB_Message.Text = "Versuchen Sie nochmal oder registrieren sich";

                }

                //this.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Arbeitsspeicher Erzeugung" + ex.Message);
                throw;
            }
        }

        private void BT_Register_Click(object sender, RoutedEventArgs e)
        {
            Views.Register registration = new Views.Register();
            registration.Show();
        }

        private void BT_Logout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BT_ExamCall_Click(object sender, RoutedEventArgs e)
        {
           
            // look for the voucher to get the exam-id
            Views.Exams exam = new Views.Exams(user);
            exam.Show();



            // get all questions for this exam-id in the grid 


            // questions bearbeiten und save them
        }
    }
}
