using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CandidateWPF.Model
{
    public class User
    {
        int id;

        string firstname;

        string lastname;

        DateTime birthdate;

        string email;

        string plz;

        string street;

        int streetnumber;

        Boolean valid;

        string voucher;

        DateTime createdon;

        string city;

        public User(int Id,string Firstname, string Lastname, DateTime Birthdate,
            string Email, string Plz, string Street, int Streetnumber, string City, 
            bool Valid, string Voucher, DateTime Createdon)
        {
            id = Id;
            firstname = Firstname;
            lastname = Lastname;
            birthdate = Birthdate;
            email = Email;
            plz = Plz;
            street = Street;
            city = City;
            valid = Valid;
            voucher = Voucher;
            streetnumber = Streetnumber;
            createdon = Createdon;
        }

        public User(User User)
        {
            User user = User;
        }

        public int Id { get => id; }
        public string Firstname { get => firstname; set => firstname = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public DateTime Birthdate { get => birthdate; set => birthdate = value; }
        public string Email { get => email; set => email = value; }
        public string Plz { get => plz; set => plz = value; }
        public string Street { get => street; set => street = value; }
        public string City { get => city;}
        public int Streetnumber { get => streetnumber; set => streetnumber = value; }
        public bool Valid { get => valid; set => valid = value; }
        public string Voucher { get => voucher; set => voucher = value; }
        public DateTime Createdon { get => createdon; set => createdon = value; }
    }

}
