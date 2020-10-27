using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAccounting
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Staff staff1 = new Staff("Kyle", 32.00f);
            staff1.HoursWorked = 10;
            staff1.CalculatePay();
            Console.WriteLine(staff1.TotalPay);
            Console.WriteLine(staff1.ToString());
            */

            Console.ReadKey();
        }
    }

    class Staff
    {
        private float hourlyRate;
        private int hWorked;

        public float TotalPay { get; protected set; }
        public float BasicPay { get; private set; }
        public string NameOfStaff { get; private set; }

        public int HoursWorked
        {
            get
            {
                return hWorked;
            }
            set
            {
                if (value > 0)
                {
                    hWorked = value;
                }
                else
                {
                    hWorked = 0;
                }
            }
        }

        public Staff(string name, float rate)
        {
            NameOfStaff = name;
            hourlyRate = rate;
        }
        
        public virtual void CalculatePay()
        {
            Console.WriteLine("Calculating Pay...");
            BasicPay = hWorked * hourlyRate;
            TotalPay = BasicPay;
        }

        public override string ToString()
        {
            return "hourlyRate = " + hourlyRate + " hWorked = " + hWorked + " TotalPay = " + TotalPay + " BasicPay = " + BasicPay + " NameOfStaff = " + NameOfStaff;
        }

    }

    // class Manager : Staff { }
    // class Admin : Staff { }
    // class FileReader { }
    // class PaySlip { }
}
