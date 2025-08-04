using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeContactSystem.Domain.Entities
{

    /// <summary>
    /// Represents an employee in the system.
    /// </summary>
    public class Employee
    {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string JobTitle { get; set; } = string.Empty;

        public int CompanyID { get; set; }

        ///Navigation from Employee to Company
        public Company Company { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
