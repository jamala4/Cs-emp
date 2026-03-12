using System;

namespace EmpDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee e1 = new SalariedEmployee("John", "Doe", "john@email.com", 1200m);
            Employee e2 = new HourlyEmployee("Jane", "Smith", "jane@email.com", 25m, 40m);
            Employee e3 = new CommissionEmployee("Mike", "Lee", "mike@email.com", 5000m, 0.10m);
            Employee e4 = new BasePlusCommissionEmployee("Sara", "Khan", "sara@email.com", 4000m, 0.08m, 300m);

            Console.WriteLine(e1);
            Console.WriteLine("Pay: " + e1.Earnings().ToString("C"));
            Console.WriteLine();

            Console.WriteLine(e2);
            Console.WriteLine("Pay: " + e2.Earnings().ToString("C"));

            Console.WriteLine();
            Console.WriteLine(e3);
            Console.WriteLine("Pay: " + e3.Earnings().ToString("C"));

            Console.WriteLine();
            Console.WriteLine(e4);
            Console.WriteLine("Pay: " + e4.Earnings().ToString("C"));

            Console.ReadLine();
        }
    }
}