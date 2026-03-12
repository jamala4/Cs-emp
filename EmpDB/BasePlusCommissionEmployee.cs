using System;

namespace EmpDB
{
    class BasePlusCommissionEmployee : Employee
    {
        public decimal GrossSales { get; set; }
        public decimal CommissionRate { get; set; }
        public decimal BaseSalary { get; set; }

        public BasePlusCommissionEmployee(string firstName, string lastName, string emailAddress, decimal grossSales, decimal commissionRate, decimal baseSalary)
            : base(firstName, lastName, emailAddress)
        {
            GrossSales = grossSales;
            CommissionRate = commissionRate;
            BaseSalary = baseSalary;
        }

        public override decimal Earnings()
        {
            return BaseSalary + (GrossSales * CommissionRate);
        }

        public override string ToString()
        {
            return "Base Plus Commission | " + base.ToString() + " | Base Salary: " + BaseSalary.ToString("C");
        }
    }
}