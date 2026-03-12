using System;

namespace EmpDB
{
    class SalariedEmployee : Employee
    {
        public decimal WeeklySalary { get; set; }

        public SalariedEmployee(string firstName, string lastName, string emailAddress, decimal weeklySalary)
            : base(firstName, lastName, emailAddress)
        {
            WeeklySalary = weeklySalary;
        }

        public override decimal Earnings()
        {
            return WeeklySalary;
        }

        public override string ToString()
        {
            return "Salaried | " + base.ToString() + " | Weekly Salary: " + WeeklySalary.ToString("C");
        }
    }
}