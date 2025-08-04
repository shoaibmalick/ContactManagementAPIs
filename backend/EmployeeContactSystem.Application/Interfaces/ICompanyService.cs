using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeContactSystem.Application.DTO;


namespace EmployeeContactSystem.Application.Interfaces
{
        /// <summary>
        /// Contract for employee-related business operations.
        /// </summary>
        public interface ICompanyService
        {
            Task<IEnumerable<CompanyDto>> GetCompanies(string? search = null, int page = 1, int pageSize = 10);

        }
}

