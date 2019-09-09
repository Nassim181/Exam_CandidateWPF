using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CandidateWPF.Model
{
    public class Exams
    {
        public int id;
        string name;
        string description;
        int category_id;
        double price;
        string category;

        public Exams(int Id, string Name, string Description, int Category_id, double Price, string Category)
        {
            id = Id;
            name = Name;
            description = Description;
            category_id = Category_id;
            price = Price;
            category = Category;
        }

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public double Price { get => price; set => price = value; }
        public string Category { get => category; }
    }
}