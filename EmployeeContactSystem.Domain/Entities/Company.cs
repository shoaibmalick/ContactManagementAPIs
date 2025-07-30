using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeContactSystem.Domain.Entities
{
    /// <summary>
    /// Represents a company in the system.
    /// </summary>
    public class Company
    {
        public int ID { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string Domain { get; set; } = string.Empty;

        public string Industry { get; set; } = string.Empty;

        public string Website { get; set; } = string.Empty;

        //Navigation from Company to List of Employees
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
