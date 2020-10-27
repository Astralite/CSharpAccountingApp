using System;
using System.IO;
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

            FileReader file = new FileReader();
            List<Staff> myStaff = file.ReadFile();
            PaySlip paySlip = new PaySlip(1,2020);
            paySlip.GeneratePaySlips(myStaff);

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

    class FileReader
    {
        public List<Staff> ReadFile()
        {
            List<Staff> myStaff = new List<Staff>();
            string[] result = new string[2];
            string path = "staff.txt";
            string[] separators = { ", " };

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        result = line.Split(separators, StringSplitOptions.None);
                        switch (result[1])
                        {
                            case "Manager":
                                myStaff.Add(new Manager(result[0]));
                                break;
                            case "Admin":
                                myStaff.Add(new Admin(result[0]));
                                break;
                            default:
                                myStaff.Add(new Staff(result[0], 25));
                                break;
                        }
                    }
                    sr.Close();
                }
            }
            else
            {
                Console.WriteLine("File staff.txt not found");
            }

            return myStaff;
        }
    }

    class PaySlip
    {
        private int month;
        private int year;

        enum MonthsOfYear
        {
            JAN=1,
            FEB=2,
            MAR=3,
            APR=4,
            MAY=5,
            JUN=6,
            JUL=7,
            AUG=8,
            SEP=9,
            OCT=10,
            NOV=11,
            DEC=12
        } // enums are private by default

        public PaySlip(int payMonth, int payYear)
        {
            month = payMonth;
            year = payYear;
        }

        public void GeneratePaySlips(List<Staff> myStaff)
        {
            string path;
            foreach (Staff staff in myStaff)
            {
                path = staff.NameOfStaff + ".txt";
                StreamWriter sw = new StreamWriter(path);

                Console.WriteLine("How many hours did {0} work?: ", staff.NameOfStaff);
                string userInput = Console.ReadLine();
                int hoursWorked = 0;
                Int32.TryParse(userInput, out hoursWorked);
                staff.HoursWorked = hoursWorked;
                staff.CalculatePay();
                sw.WriteLine("PAYSLIP FOR {0} {1}", (MonthsOfYear)month, year);
                sw.WriteLine("=====================");
                sw.WriteLine("Name of Staff: {0}", staff.NameOfStaff);
                sw.WriteLine("Hours Worked: {0}", staff.HoursWorked);
                sw.WriteLine("");
                sw.WriteLine("Basic Pay: {0:C2}", staff.BasicPay);
                if (staff.GetType() == typeof(Manager))
                {
                    sw.WriteLine("Allowance: {0:C2}", ((Manager)staff).Allowance);
                }
                if (staff.GetType() == typeof(Admin))
                {
                    sw.WriteLine("Overtime: {0:C2}", ((Admin)staff).Overtime);
                }
                sw.WriteLine("");
                sw.WriteLine("=====================");
                sw.WriteLine("Total Pay: {0:C2}", staff.TotalPay);
                sw.WriteLine("=====================");

                sw.Close();
            }
        }
    }
}
