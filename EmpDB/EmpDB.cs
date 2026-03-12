using System.Collections.Generic;

namespace EmpDB
{
    class EmpDB
    {
        public List<Employee> Employees { get; set; }

        public EmpDB()
        {
            Employees = new List<Employee>();
        }
    }
}