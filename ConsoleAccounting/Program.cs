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
            Staff staff1 = new Manager("Kyle");
            staff1.HoursWorked = 160;
            staff1.CalculatePay();
            Console.WriteLine(staff1.TotalPay);
            Console.WriteLine(staff1.ToString());
            */

            //Console.ReadKey();
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

    class Manager : Staff
    {
        private const float managerHourlyRate = 50;

        public int Allowance { get; private set; }

        public Manager(string name) : base(name, managerHourlyRate) { }

        public override void CalculatePay()
        {
            base.CalculatePay();
            Allowance = 1000;
            if (HoursWorked > 160)
            {
                TotalPay += Allowance;
            }
        }

        public override string ToString()
        {
            return base.ToString() + " Allowance = " + Allowance;
        }
    }

    class Admin : Staff
    {
        private float overtimeRate = 15.5f;
        private const float adminHourlyRate = 30f;

        public float Overtime { get; private set; }

        public Admin(string name) : base(name, adminHourlyRate) { }

        public override void CalculatePay()
        {
            base.CalculatePay();
            if (HoursWorked > 160)
            {
                Overtime = (HoursWorked - 160) * overtimeRate;
                TotalPay += Overtime;
            }
        }

        public override string ToString()
        {
            return base.ToString() + " Overtime = " + Overtime;
        }
    }

    // class FileReader { }
    // class PaySlip { }
}
