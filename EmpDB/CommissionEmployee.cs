using System;

namespace EmpDB
{
    class CommissionEmployee : Employee
    {
        public decimal GrossSales { get; set; }
        public decimal CommissionRate { get; set; }

        public CommissionEmployee(string firstName, string lastName, string emailAddress, decimal grossSales, decimal commissionRate)
            : base(firstName, lastName, emailAddress)
        {
            GrossSales = grossSales;
            CommissionRate = commissionRate;
        }

        public override decimal Earnings()
        {
            return GrossSales * CommissionRate;
        }

        public override string ToString()
        {
            return "Commission | " + base.ToString() + " | Gross Sales: " + GrossSales.ToString("C") + " | Rate: " + CommissionRate;
        }
    }
}