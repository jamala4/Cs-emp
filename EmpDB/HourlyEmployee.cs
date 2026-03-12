using System;

namespace EmpDB
{
    class HourlyEmployee : Employee
    {
        public decimal Wage { get; set; }
        public decimal Hours { get; set; }

        public HourlyEmployee(string firstName, string lastName, string emailAddress, decimal wage, decimal hours)
            : base(firstName, lastName, emailAddress)
        {
            Wage = wage;
            Hours = hours;
        }

        public override decimal Earnings()
        {
            return Wage * Hours;
        }

        public override string ToString()
        {
            return "Hourly | " + base.ToString() + " | Wage: " + Wage.ToString("C") + " | Hours: " + Hours;
        }
    }
}