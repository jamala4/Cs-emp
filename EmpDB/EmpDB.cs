using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;

namespace EmpDB
{
    internal class EmpDB
    {

        private List<Employee> employees = new List<Employee>();

        public EmpDB()
        {
            ReadEmployeeDataFromInputFile();
        }

        private const string Employee_INPUTFILE = "employees.txt";


        private void ReadEmployeeDataFromInputFile()
        {
            StreamReader inFile = new StreamReader(Employee_INPUTFILE);

            string employeeType = string.Empty;

            while ((employeeType = inFile.ReadLine()) != null)
            {

                string firstName = inFile.ReadLine();
                string lastName = inFile.ReadLine();
                string emailAddress = inFile.ReadLine();


                if (employeeType == "Hourly")
                {
                    decimal wage = decimal.Parse(inFile.ReadLine());
                    decimal hours = decimal.Parse(inFile.ReadLine());

                    Employee hourly = new HourlyEmployee(firstName, lastName, emailAddress, wage, hours);
                    employees.Add(hourly);
                }
                //if the student type is grad student, we read the specific attributes for a grad student and create a grad student object and add it to the list
                else if (employeeType == "Salaried")

                {
                    decimal weeklySalary = decimal.Parse(inFile.ReadLine());

                    Employee salary = new SalariedEmployee(firstName, lastName, emailAddress, weeklySalary);
                    employees.Add(salary);

                }
                else if (employeeType == "Comission")

                {
                    decimal grossSales = decimal.Parse(inFile.ReadLine());
                    decimal commissionRate = decimal.Parse(inFile.ReadLine());

                    Employee comission = new CommissionEmployee(firstName, lastName, emailAddress, grossSales, commissionRate);
                    employees.Add(comission);

                }
                else if (employeeType == "BasePlusCommission")

                {
                    decimal grossSales = decimal.Parse(inFile.ReadLine());
                    decimal commissionRate = decimal.Parse(inFile.ReadLine());
                    decimal baseSalary = decimal.Parse(inFile.ReadLine());

                    Employee basePlusCommission = new BasePlusCommissionEmployee(firstName, lastName, emailAddress, grossSales, commissionRate, baseSalary);
                    employees.Add(basePlusCommission);

                }
                //if the student type is not recognized, we can skip the record and move on to the next one
                else
                {
                    Console.WriteLine($"Error: Invalid employee type {employeeType} in input file. Skipping record.");
                }

            }
            //close the file 
            inFile.Close();
        }

        //main db engine loop
        public void GoDataBase()
        {

            while (true)
            {
                //disply the main menu
                DisplayMainMenu();

                //capture the user selection
                char selection = GetUserSelection();

                //decision branching the various operations
                switch (selection)
                {
                    case 'C':
                    case 'c':
                        //create a new student record
                        CreateNewEmployeeRecord();
                        break;
                    case 'F':
                    case 'f':
                        //find an existing student record
                        string email = string.Empty;
                        Employee emp = FindEmployeeRecord(out email);
                        if (emp != null) Console.WriteLine(emp);
                        break;
                    case 'U':
                    case 'u':
                        //update an existing student record
                        UpdateEmployeeRecord();
                        break;
                    case 'D':
                    case 'd':
                        //delete an existing student record
                        DeleteEmployeeRecord();
                        break;

                    case 'S':
                    case 's':
                        //this only saves and continues 
                        SaveStudentDataOutputFile();
                        break;
                    case 'Q':
                    case 'q':
                        //Save and exit app
                        SaveStudentDataOutputFile();
                        Environment.Exit(0);
                        break;
                    case 'E':
                    case 'e':
                        //Exit the environment without saving
                        Environment.Exit(0);
                        break;
                    case 'P':
                    case 'p':
                        //Print all student records to the console
                        PayEmployee();
                        break;
                    default:
                        Console.Write($"Error: {selection} is not a valid selection. Please try again...");
                        break;
                }
            }
        }

        //This method creates a new student record by first checking if a record with the same email already exists.
        //If it does not exist, it prompts the user for the necessary information to create either an Undergrad or GradStudent record and adds it to the students list.
        //If a record with the same email already exists, it informs the user that a duplicate record cannot be created.
        private void CreateNewEmployeeRecord()
        {
            //First- get the search email value from user  
            string email = string.Empty;
            Employee emp = FindEmployeeRecord(out email);

            //best scenario is stu to come back from search as null 
            if (emp == null)
            {   //create a new student record and add to the list 
                Console.WriteLine($"\n Creating new student record with email {email}...");

                Console.Write("Enter first name: ");
                string FirstName = Console.ReadLine();

                Console.Write("Enter last name: ");
                string LastName = Console.ReadLine();


                //What kind of student is this? Undergrad or GradStudent?
                Console.Write("[S]alary, [H]ourly, [C]ommission, [B]asePlusCommission: ");
                char employeeType = GetUserSelection();

                //Based on the student type, we will read the specific attributes for that type and create the appropriate student object and add it to the list
                if (employeeType == 'S' || employeeType == 's')
                {
                    Console.Write("\nEnter weekly salary: ");
                    decimal weeklySalary = decimal.Parse(Console.ReadLine());

                    emp = new SalariedEmployee(FirstName, LastName, email, weeklySalary);
                    employees.Add(emp);
                }
                //if the student type is grad student, we read the specific attributes for a grad student and create a grad student object and add it to the list
                else if (employeeType == 'H' || employeeType == 'h')
                {
                    Console.WriteLine("\nEnter hourly rate: ");
                    decimal wage = decimal.Parse(Console.ReadLine());

                    decimal hours = 0;

                    emp = new HourlyEmployee(FirstName, LastName, email, wage, hours);
                    employees.Add(emp);
                }
                else if (employeeType == 'C' || employeeType == 'c')
                {
                    Console.WriteLine("\nEnter gross sales: ");
                    decimal grossSales = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("\nEnter commission rate: ");
                    decimal commissionRate = decimal.Parse(Console.ReadLine());

                    emp = new CommissionEmployee(FirstName, LastName, email, grossSales, commissionRate);
                    employees.Add(emp);
                }
                else if (employeeType == 'B' || employeeType == 'b')
                {
                    Console.WriteLine("\nEnter gross sales: ");
                    decimal grossSales = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("\nEnter commission rate: ");
                    decimal commissionRate = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("\nEnter base salary: ");
                    decimal baseSalary = decimal.Parse(Console.ReadLine());

                    emp = new BasePlusCommissionEmployee(FirstName, LastName, email, grossSales, commissionRate, baseSalary);
                    employees.Add(emp);
                }
                else
                {
                    Console.WriteLine($"\n ERROR: {employeeType} is not a valid student type. Record FAILED! " +
                                      $"\n{email} is still available");
                }

            }
            else
            {
                //if stu is not null, then a record with the email already exists and we cannot create a duplicate record
                Console.WriteLine($"\n A record with email {email} already exists. Cannot create duplicate record.");
            }
        }

        //This method finds a student record based on the email address provided by the user.
        //It iterates through the students list and returns the matching student record if found.
        //If no match is found, it informs the user and returns null.
        private Employee FindEmployeeRecord(out string email)
        {
            //first get search email value from user 
            Console.Write("\nEnter the email address (primary key) to search for: ");
            email = Console.ReadLine();

            //iterate through the array list 
            foreach (Employee emp in employees)
            {
                if (email == emp.EmailAddress)
                {
                    Console.WriteLine($"\n FOUND student record: {emp.EmailAddress}");
                    return emp;
                }
            }
            //if no match was found 
            Console.WriteLine($"\n {email} NOT FOUND.");

            return null;
        }

        //This method updates an existing student record by first finding the record based on the email address provided by the user.
        private void UpdateEmployeeRecord()
        {
            string email;
            Employee emp = FindEmployeeRecord(out email);
            //if stu is null, then no record was found for the email and we cannot update a non-existent record
            if (emp == null)
            {
                Console.WriteLine($"\nCannot update. No record found for {email}.");
                return;
            }

            Console.WriteLine("\nUpdating student record...");

            Console.WriteLine("\n Would you like to change the first name? [Y/N]: ");
            char userSelection = GetUserSelection();
            //if the user selects yes, we prompt for the new first name and update the record if a new name is provided
            if (userSelection == 'Y' || userSelection == 'y')
            {

                Console.Write("\n Enter new first name (leave blank to keep current): ");
                string firstName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(firstName))
                    emp.FirstName = firstName;

            }
            //  if the user selects no, we keep the current first name and move on to the next attribute
            else if (userSelection == 'N' || userSelection == 'n')
            {
                Console.WriteLine("\n Keeping current first name.");
            }
            else
            {
                Console.WriteLine($"\n Invalid selection {userSelection}. Keeping current first name.");
            }
            //we repeat the same process for the last name and GPA attributes, prompting the user for each one and updating if they choose to change it
            Console.WriteLine("\n Would you like to change the last name? [Y/N]: ");
            userSelection = GetUserSelection();
            //if the user selects yes, we prompt for the new last name and update the record if a new name is provided
            if (userSelection == 'Y' || userSelection == 'y')
            {

                Console.Write("\n Enter new last name (leave blank to keep current): ");
                string lastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(lastName))
                    emp.LastName = lastName;

            }
            //  if the user selects no, we keep the current last name and move on to the next attribute
            else if (userSelection == 'N' || userSelection == 'n')
            {
                Console.WriteLine("\n Keeping current last name.");
            }
            else
            {
                Console.WriteLine($"\n Invalid selection {userSelection}. Keeping current last name.");
            }

            //after handling the common attributes, we check the type of student record and handle the subtype-specific attributes accordingly
            // Handle subtype-specific updates
            if (emp is HourlyEmployee hourly)
            {
                Console.Write("\n Enter new wage (leave blank to keep current): ");
                string wage = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(wage))
                    hourly.Wage = decimal.Parse(wage);

            }
            //if the student is a grad student, we prompt for the new credit amount and advisor name and update the record if new values are provided
            else if (emp is SalariedEmployee salary)
            {
                Console.Write("\n Enter new weekly salary (leave blank to keep current): ");
                string sal = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(sal))
                    salary.WeeklySalary = decimal.Parse(sal);

            }
            else if (emp is CommissionEmployee Commission)
            {
                Console.Write("\n Enter new rate of commission (less than .20): ");
                string comRate = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(comRate))
                    Commission.CommissionRate = decimal.Parse(comRate);

            }
            else if (emp is BasePlusCommissionEmployee BasePlusCom)
            {
                Console.Write("\n Enter new rate of commission (less than .20): ");
                string BasPlCom = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(BasPlCom))
                    BasePlusCom.CommissionRate = decimal.Parse(BasPlCom);

                Console.Write("\n Enter new base salary: ");
                string baseSal = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(baseSal))
                    BasePlusCom.BaseSalary = decimal.Parse(baseSal);

            }

            Console.WriteLine("\nStudent record updated successfully.");
            Console.WriteLine("*********************");
            Console.WriteLine(emp);
            Console.WriteLine("*********************");
        }

        //This method deletes an existing student record by first finding the record based on the email address provided by the user.
        private void DeleteEmployeeRecord()
        {
            string email;
            Employee emp = FindEmployeeRecord(out email);
            //if stu is null, then no record was found for the email and we cannot delete a non-existent record
            if (emp != null)
            {
                //if a record is found, we prompt the user to confirm the deletion and if they confirm, we remove the record from the students list
                while (true)
                {
                    Console.WriteLine($"\n Are you sure you want to delete the employee record with email {email}? [Y/N]: ");
                    char userAnswer = GetUserSelection();

                    if (userAnswer == 'Y' || userAnswer == 'y')
                    {
                        employees.Remove(emp);
                        Console.WriteLine($"\nEmployee record with email {email} has been deleted.");
                        break;
                    }
                    else if (userAnswer == 'N' || userAnswer == 'n')
                    {
                        Console.WriteLine($"\nDeletion cancelled. Employee record with email {email} has NOT been deleted.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\nInvalid selection {userAnswer}");
                    }
                }

            }
            else
            {
                Console.WriteLine($"\nCannot delete. No record found for {email}.");
            }
        }

        private const string EMPLOYEE_OUTPUTFILE = "EMPLOYEE_OUTPUT_FILE__.txt";
        //This method saves all the student records in the students list to an output file by writing each record's string representation to the file.
        private void SaveStudentDataOutputFile()
        {
            //create file object and connect to actual file 

            StreamWriter outFile = new StreamWriter(EMPLOYEE_OUTPUTFILE);

            //use it -- same as any write or reader
            foreach (Employee emp in employees)
            {
                outFile.WriteLine(emp.ToStringForOutputFile());
                Console.WriteLine(emp.ToStringForOutputFile());

            }

            //release the resource -- DO NOT FORGET TO CLOSE
            outFile.Close();
        }
        //This method prints all the student records in the students list to the console by iterating through the list and writing each record's string representation to the console.
        private void PayEmployee()
        {

        }
        //  This method captures the user's selection from the main menu by reading a key press from the console and returning the character representation of the key pressed.
        private char GetUserSelection()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            return keyInfo.KeyChar;
        }


        //This method displays the main menu options to the user by writing a formatted string to the console that
        //outlines the various operations that can be performed on the student database.
        private void DisplayMainMenu()
        {
            Console.Write(@"
*******************************************
********** Payroll Database Menu **********
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
[C]reate a new employee record
[F]ind an existing employee record
[P]ay out an employee
[U]pdate an exisiting employee record
[D]elete an existing employee record
[E]xit the application w/o saving
[S]ave all changes and continue...
[Q]uit the application after saving
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

User Selection: ");
        }
    }
}