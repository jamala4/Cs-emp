using System;
using System.Globalization;
using System.IO;

namespace EmpDB
{
    class EmpDBApp
    {
        private EmpDB db;
        private const string FileName = "employees.txt";

        public EmpDBApp()
        {
            db = new EmpDB();
        }

        public void Run()
        {
            LoadFromFile();
            ProcessPayroll();
            SaveToFile();
            Console.ReadLine();
        }

        public void LoadFromFile()
        {
            db.Employees.Clear();

            if (!File.Exists(FileName))
            {
                Console.WriteLine("employees.txt not found.");
                return;
            }

            string[] lines = File.ReadAllLines(FileName);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(',');

                try
                {
                    string type = parts[0].Trim().ToLower();

                    if (type == "salaried")
                    {
                        db.Employees.Add(new SalariedEmployee(
                            parts[1],
                            parts[2],
                            parts[3],
                            decimal.Parse(parts[4], CultureInfo.InvariantCulture)
                        ));
                    }
                    else if (type == "hourly")
                    {
                        db.Employees.Add(new HourlyEmployee(
                            parts[1],
                            parts[2],
                            parts[3],
                            decimal.Parse(parts[4], CultureInfo.InvariantCulture),
                            decimal.Parse(parts[5], CultureInfo.InvariantCulture)
                        ));
                    }
                    else if (type == "commission")
                    {
                        db.Employees.Add(new CommissionEmployee(
                            parts[1],
                            parts[2],
                            parts[3],
                            decimal.Parse(parts[4], CultureInfo.InvariantCulture),
                            decimal.Parse(parts[5], CultureInfo.InvariantCulture)
                        ));
                    }
                    else if (type == "basepluscommission")
                    {
                        db.Employees.Add(new BasePlusCommissionEmployee(
                            parts[1],
                            parts[2],
                            parts[3],
                            decimal.Parse(parts[4], CultureInfo.InvariantCulture),
                            decimal.Parse(parts[5], CultureInfo.InvariantCulture),
                            decimal.Parse(parts[6], CultureInfo.InvariantCulture)
                        ));
                    }
                }
                catch
                {
                    Console.WriteLine("Skipped bad line: " + line);
                }
            }

            Console.WriteLine("Loaded " + db.Employees.Count + " employees.");
        }

        public void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter(FileName))
            {
                foreach (Employee emp in db.Employees)
                {
                    if (emp is SalariedEmployee salaried)
                    {
                        writer.WriteLine("Salaried," + salaried.FirstName + "," + salaried.LastName + "," +
                                         salaried.EmailAddress + "," + salaried.WeeklySalary.ToString(CultureInfo.InvariantCulture));
                    }
                    else if (emp is HourlyEmployee hourly)
                    {
                        writer.WriteLine("Hourly," + hourly.FirstName + "," + hourly.LastName + "," +
                                         hourly.EmailAddress + "," + hourly.Wage.ToString(CultureInfo.InvariantCulture) + "," +
                                         hourly.Hours.ToString(CultureInfo.InvariantCulture));
                    }
                    else if (emp is CommissionEmployee commission)
                    {
                        writer.WriteLine("Commission," + commission.FirstName + "," + commission.LastName + "," +
                                         commission.EmailAddress + "," + commission.GrossSales.ToString(CultureInfo.InvariantCulture) + "," +
                                         commission.CommissionRate.ToString(CultureInfo.InvariantCulture));
                    }
                    else if (emp is BasePlusCommissionEmployee basePlus)
                    {
                        writer.WriteLine("BasePlusCommission," + basePlus.FirstName + "," + basePlus.LastName + "," +
                                         basePlus.EmailAddress + "," + basePlus.GrossSales.ToString(CultureInfo.InvariantCulture) + "," +
                                         basePlus.CommissionRate.ToString(CultureInfo.InvariantCulture) + "," +
                                         basePlus.BaseSalary.ToString(CultureInfo.InvariantCulture));
                    }
                }
            }

            Console.WriteLine("Saved " + db.Employees.Count + " employees.");
        }

        public void ProcessPayroll()
        {
            decimal totalPayroll = 0m;

            Console.WriteLine();
            Console.WriteLine("==== Payroll Report ====");

            foreach (Employee emp in db.Employees)
            {
                decimal pay = emp.Earnings();
                totalPayroll += pay;

                Console.WriteLine(emp);
                Console.WriteLine("Pay: " + pay.ToString("C"));
                Console.WriteLine();
            }

            Console.WriteLine("Total Payroll: " + totalPayroll.ToString("C"));
        }
    }
}
