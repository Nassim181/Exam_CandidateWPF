using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Interaktionslogik für Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        Model.User user;
        Boolean register = false;
        public Register()
        {
            InitializeComponent();
        }

        private void BT_register_Click(object sender, RoutedEventArgs e)
        {
            TB_Message.Visibility = Visibility.Hidden;

            try
            {
                TB_Message.Visibility = Visibility.Hidden;
                int id = 1;
                string fname = TB_Firstname.Text;
                string lname = TB_Lastname.Text;
                DateTime birth = Convert.ToDateTime(TB_Birthdate.Text);

                string email = TB_Email.Text;
                if (email.Length == 0)
                {
                    TB_Message.Visibility = Visibility.Visible;
                    TB_Message.Text = "Email is required";
                }

                string city = TB_City.Text;

                string plz = TB_PLZ.Text;

                string street = TB_Street.Text;

                int streetnr = Convert.ToInt32(TB_Streetnummer.Text);

                bool valid = true;

                int validint = 0;

                string voucher = TB_Voucher.Text;

                DateTime createdon = DateTime.Now;


                string password = TB_Password.Password.ToString();
                string passwordwdr = TB_Passowrdwrd.Password.ToString();

                if (password != passwordwdr)
                {
                    TB_Message.Visibility = Visibility.Visible;
                    TB_Message.Text = "Passwords sollen übereinstimmen";
                    register = false;
                }

                user = new Model.User(id, fname, lname, birth, email, plz, street, streetnr, city, valid, voucher, createdon);

                Boolean CreationResult = Database.DBConnection.DB_CreateUser(fname, lname, birth, email, voucher, createdon,
                    city, street, streetnr, plz, valid, password);
                if (CreationResult)
                {
                    //this.Close();
                    TB_Message.Visibility = Visibility.Visible;
                    TB_Message.Text = "Registrierung Erfolgreich...Logged sie sich ein!";
                    //TB_Message.Background("#FF1BDC35");
                    register = true;
                    //Views.Login();

                }
                else
                {
                    //this.Close();
                    TB_Message.Visibility = Visibility.Visible;
                    TB_Message.Text = "Sie sind schon registriert...melden sie sich!";
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Arbeitsspeicher Erzeugung" + ex.Message);
                throw;
            }

        }

        private void BT_Close_Click(object sender, RoutedEventArgs e)
        {
            if (register)
            {
                BT_Close.IsEnabled = true;
                this.Close();
            }
        }
    }
}