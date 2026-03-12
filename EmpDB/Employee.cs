using System;

namespace EmpDB
{
    abstract class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public Employee(string firstName, string lastName, string emailAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }

        public abstract decimal Earnings();

        public override string ToString()
        {
            return FirstName + " " + LastName + " | " + EmailAddress;
        }
    }
}